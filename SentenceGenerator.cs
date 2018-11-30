using Markov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTyper
{
    public class SentenceGenerator
    {
        private MarkovChain<string> _chain;
        private Random _rand = new Random();

        private string _sentenceBase =
@"Biographical works are usually non-fiction, but fiction can also be used to portray a person's life. One in-depth form of biographical coverage is called legacy writing. Works in diverse media, from literature to film, form the genre known as biography. Engraving facing the title page of an 18th-century edition of Plutarch's Lives. Plutarch\'s Lives of the Noble Greeks and Romans, commonly called Parallel Lives or Plutarch's Lives, is a series of biographies of famous men, arranged in tandem to illuminate their common moral virtues or failings, probably written at the beginning of the second century AD. Plutarch claims that many slaves and fugitives were already following the twins when they set forth and were motivated by the Alban unwillingness to allow their cohorts to remain. He adds that some sources indicate that Romulus lied about the 12 birds he saw during the contest with Remus.Remus was killed either by his brother, or Celer, Romulus' man, who then fled to Tuscany with so much haste that his name became the Latin word for speed. Also killed was Faustulus' brother Pleistinus. The Ramayana is one of the largest ancient epics in world literature. It consists of nearly 24,000 verses (mostly set in the Shloka meter), divided into seven Kandas and about 500 sargas (chapters). In Hindu tradition, it is considered to be the adi-kavya (first poem). It depicts the duties of relationships, portraying ideal characters like the ideal father, the ideal servant, the ideal brother, the ideal husband and the ideal king. Ramayana was an important influence on later Sanskrit poetry and Hindu life and culture. Like Mahabharata, Ramayana is not just a story: it presents the teachings of ancient Hindu sages in narrative allegory, interspersing philosophical and ethical elements. The characters Rama, Sita, Lakshmana, Bharata, Hanuman, Shatrughna, and Ravana are all fundamental to the cultural consciousness of India, Nepal, Sri Lanka and south-east Asian countries such as Thailand, Cambodia, Malaysia and Indonesia. Rama Meets Sugreeva Vali ruled the kingdom of Kishkindha; his subjects were the vanaras. Tara was his wife. One day, a raging demon by the name of Maayaavi came to the gates of the capital and challenged Vali to a fight. Vali accepted the challenge, but when he sallied forth, the demon fled in terror into a deep cave. Vali entered the cave in pursuit of the demon, telling Sugriva to wait outside. When Vali did not return and upon hearing demonic shouts in the cave and seeing blood oozing from its mouth, Sugriva concluded that his brother had been killed. With a heavy heart, Sugriva rolled a boulder to seal the cave's opening, returned to Kishkindha and assumed kingship over the vanaras. Vali, however, ultimately prevailed in his combat with the demon and returned home. Seeing Sugriva acting as king, he concluded that his brother had betrayed him.";

        public SentenceGenerator()
        {
            _chain = new MarkovChain<string>(1);

            foreach (var statement in _sentenceBase.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
            {
                _chain.Add(statement.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), 1);
            }
        }

        public string GetRandomStatement(int minimumWords = 10)
        {
            var sb = new StringBuilder();

            while (sb.ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < minimumWords)
                sb.Append(string.Join(" ", _chain.Chain(_rand)));

            return sb.ToString();
        }
    }
}
