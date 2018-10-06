// 无锁且线程安全的普通TrieTree
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;

namespace IMBA_TJD.TrieTree
{
    public class TrieTree
    {
        TrieTreeNode root;
        int depth; // A property or indexer may not be passed as an out or ref parameter.
        public int Depth => depth; // 不含'\0'的深度

        class TrieTreeNode
        {
            char character;
            ConcurrentDictionary<char, TrieTreeNode> childs;
            internal bool IsEnd => character == '\0';
            internal TrieTreeNode(char character = '\0') // 直接调用仅用于根节点
            {
                this.character = character;
                childs = new ConcurrentDictionary<char, TrieTreeNode>();
            }
            internal TrieTreeNode this[char key] => childs[key];
            internal bool HasChild(char character) => childs.ContainsKey(character);
            internal TrieTreeNode AddAndGetChild(char character) => childs.GetOrAdd(character, new TrieTreeNode(character));
            //internal bool AddChild(TrieTreeNode node) => childs.TryAdd(node.character, node);
            //internal TrieTreeNode AddAndGetChild(char character) // 但其实可以直接用GetOrAdd方法
            //{
            //    TrieTreeNode node = new TrieTreeNode(character);

            //    if (AddChild(node) == false) // 不能用HasChild，否则并发判断不存在后再添加，还是可能失败，却返回的是不同的node
            //        return childs[character]; // 存在

            //    return node; // 不存在，成功添加
            //}
        }

        public TrieTree()
        {
            depth = 0;
            root = new TrieTreeNode();
        }
        public TrieTree(IEnumerable<string> collection) : this() => InsertMulti(collection);

        //readonly object mutex = new object(); // 用于depth
        public bool Insert(string word)
        {
            if (word == null)
                return false;

            if (CachedMatch(word, out string unmatchedWords, out TrieTreeNode unmatchedNode) == true)
                return false;

            foreach (char character in unmatchedWords)
                unmatchedNode = unmatchedNode.AddAndGetChild(character);

            int depth_old;
            do
            {
                depth_old = depth;
                if (depth_old >= word.Length)
                    break;
            } while (depth_old != Interlocked.CompareExchange(ref depth, word.Length, depth_old)); // 如果有其他线程修改了depth，该方法不会替换depth但仍然会返回值且不等于depth_old，这样循环才能继续

            //lock (mutex)
            //{
            //    if (this.depth < word.Length)
            //        this.depth = word.Length;
            //}

            return true;
        }
        public int InsertMulti(IEnumerable<string> collection)
        {
            int counts = 0;

            Parallel.ForEach(collection, (word) =>
            {
                if (Insert(word) == true)
                    Interlocked.Increment(ref counts);
            });

            return counts;
        }
        public bool FullMatch(string word)
        {
            if (this.depth < word.Length)
                return false;
            else
                return CachedMatch(word, out _, out _);
        }
        public int PrefixMatch(string word) // returns unmatched position.
        {
            CachedMatch(word, out string unmatchedWords, out _);
            return word.Length - unmatchedWords.Length + 1; // unmatchedWords的长度算了'\0'而这里的word没有，所以需要+1
        }

        bool CachedMatch(string word, out string unmatchedWords, out TrieTreeNode unmatchedNode)
        {
            word += '\0';
            TrieTreeNode node = root;
            int position = 0;

            // position是word.Length-1时，character是'\0'，再++以后就超索引了；但完全匹配时需要它的值是word.Length才行，这样要么在循环体里加if，要么for不以character为中心。
            //for (char character = word[position]; node.HasChild(character); character = word[++position])
            //    node = node[character];

            while (position < word.Length) // 将前面的都匹配了
            {
                char character = word[position];
                if (node.HasChild(character))
                {
                    node = node[character];
                    position++;
                }
                else
                    break;
            }

            if (position == word.Length && node.IsEnd) // 匹配成功，其实只要用一个进行判断就行
            {
                unmatchedWords = "\0"; // 不能是null，需要它的Length属性；也不能是"",因为不匹配的时候算了'\0'
                unmatchedNode = null;
                return true;
            }
            else // 因为词语加上了'\0'，所以剩下的其实都是同一种情况。词语不可能完全是树里词语的前缀，必定有剩；结点也不可能在'\0'处不匹配。只要处理剩下的部分就行了
            {
                unmatchedWords = word.Substring(position);
                unmatchedNode = node;
                return false;
            }
        }
    }
}
