namespace ERP.Web.ViewModels
{
    public class ReportHeader
    {
        public int Id { get; set; }
        public string ReportType { get; set; }

        public int CompanyNameLeft { get; set; }
        public int CompanyNameHeight { get; set; }
        public int CompanyNameTop { get; set; }
        public int CompanyNameWidth { get; set; }
        public int CompanyNameFontSize { get; set; }
        public bool CompanyNameBold { get; set; }
        public string CompanyNameAlign { get; set; }

        public int CompanyAddressLeft { get; set; }
        public int CompanyAddressHeight { get; set; }
        public int CompanyAddressTop { get; set; }
        public int CompanyAddressWidth { get; set; }
        public int CompanyAddressFontSize { get; set; }
        public bool CompanyAddressBold { get; set; }
        public string CompanyAddressAlign { get; set; }

        public int ReportNameLeft { get; set; }
        public int ReportNameHeight { get; set; }
        public int ReportNameTop { get; set; }
        public int ReportNameWidth { get; set; }
        public int ReportNameFontSize { get; set; }
        public bool ReportNameBold { get; set; }
        public string ReportNameAlign { get; set; }

        public int CompanyLogoLeft { get; set; }
        public int CompanyLogoHeight { get; set; }
        public int CompanyLogoTop { get; set; }
        public int CompanyLogoWidth { get; set; }

        public int FirstLineLeft { get; set; }
        public int FirstLineTop { get; set; }
        public int FirstLineWidth { get; set; }
        public bool FirstLineSuppress { get; set; }

        public int SecondLineLeft { get; set; }
        public int SecondLineTop { get; set; }
        public int SecondLineWidth { get; set; }
        public bool SecondLineSuppress { get; set; }
    }
}