namespace DesignPatterns.Creational.Factory_Method
{
    public abstract class Document
    {
        private List<Page> Pages = new List<Page>();

        public Document()
        {
            this.CreatePages();
        }

        public List<Page> pages { get { return this.Pages; } }
        public abstract void CreatePages();
    }

    public class Resume : Document
    {
        public override void CreatePages()
        {
            pages.Add(new SkillsPage());
            pages.Add(new EducationPage());
            pages.Add(new ExperiencePage());
        }
    }

    public class Report : Document
    {
        public override void CreatePages()
        {
            pages.Add(new IntroductionPage());
            pages.Add(new ResultsPage());
            pages.Add(new ConclusionPage());
            pages.Add(new SummaryPage());
            pages.Add(new BibliographyPage());
        }
    }
}
