namespace DesignPatterns.Creational.Factory_Method
{
    /// <summary>
    /// Factory Method creator — defines <b>when</b> pages are created (ctor → <see cref="CreatePages"/>).
    /// <para>
    /// Subclasses (<see cref="Resume"/>, <see cref="Report"/>) decide <b>which</b> page products to add.
    /// Clients work with <c>Document</c> and never branch on concrete page lists.
    /// </para>
    /// </summary>
    public abstract class Document
    {
        private readonly List<Page> _pages = new();

        protected Document() => CreatePages();

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
