namespace DesignPatterns.Creational.Factory_Method
{
    /// <summary>
    /// Factory Method: the base type defines WHEN pages are created (ctor → CreatePages).
    /// Subclasses decide WHICH page products to add.
    /// </summary>
    public abstract class Document
    {
        private readonly List<Page> _pages = new();

        protected Document()
        {
            // Template step: always create pages on construction.
            CreatePages();
        }

        public List<Page> pages => _pages;

        /// <summary>Factory method — overridden by Resume / Report.</summary>
        public abstract void CreatePages();
    }

    /// <summary>Concrete creator: resume-specific page set.</summary>
    public class Resume : Document
    {
        public override void CreatePages()
        {
            pages.Add(new SkillsPage());
            pages.Add(new EducationPage());
            pages.Add(new ExperiencePage());
        }
    }

    /// <summary>Concrete creator: report-specific page set.</summary>
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
