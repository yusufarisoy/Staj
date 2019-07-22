using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace ArticleForm
{
    public partial class Form1 : Form
    {
        int pageNumber = 1;
        int pageSize = 11;
        int filteredPageNumber = 1;
        int filteredPageSize = 11;
        bool filter = false;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            using (var db = new nesneProjeEntities())
            {
                grid.DataSource = GridSource;
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                grid.Columns.RemoveAt(2);
                grid.Columns.RemoveAt(2);

                var articleCat = db.ArticleCategories.ToList();
                var relate = db.ArticleCategoryRelations.ToList();
                var article = db.Articles.ToList();

                //comboBox
                var categories = db.ArticleCategories.ToList();

                IEnumerable<ComboBoxItem> groups = (from cat in articleCat
                             join rel in relate on cat.ID equals rel.CategoryID
                             join art in article on rel.ArticleID equals art.ID
                            select new
                            {
                                cat,
                                rel
                            } into tables
                            group tables by tables.cat.CategoryName into grp
                            select new ComboBoxItem
                            {
                                ArticleCount = grp.Count(),
                                Text = grp.First().cat.CategoryName,
                                ID = grp.First().cat.ID
                            });

                int count = db.Articles.ToList().Count();
                string txt = "All";
                int id = 1;
                comboBox.Items.Add(new ComboBoxItem(txt, id, count));
                comboBox.Items.AddRange(groups.ToArray());

                //comboBox1
                comboBox1Items(comboBox1, pageNumber);

                label1.Visible = false;
                richTextBox1.Visible = false;
                
                comboBox.Text = "Select Category";
            }
        }
        
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           using(var db = new nesneProjeEntities())
            {

                label1.Visible = false;
                label1.Text = null;
                richTextBox1.Visible = false;
                richTextBox1.Text = null;
                grid.DataSource = null;
                grid.Rows.Clear();
                
                grid.DataSource = GridSourceFiltered();
                grid.Columns.RemoveAt(2);
                grid.Columns.RemoveAt(2);
                comboBox1Items(comboBox1, filteredPageNumber);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void Grid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        
        //previous page button
        private void Button1_Click(object sender, EventArgs e)
        {
            if(filter == false)
            {
                if (pageNumber > 0)
                {
                    pageNumber--;
                    grid.DataSource = GridSource;

                    grid.Columns.RemoveAt(2);
                    grid.Columns.RemoveAt(2);
                }
                else
                {

                }
                comboBox1Items(comboBox1, pageNumber);
            }
            else
            {
                if (pageNumber > 0)
                {
                    filteredPageNumber--;
                    grid.DataSource = GridSourceFiltered();
                    grid.Columns.RemoveAt(2);
                    grid.Columns.RemoveAt(2);
                }
                else
                {

                }
                comboBox1Items(comboBox1, filteredPageNumber);
            }
        }

        //next page button 
        private void Button3_Click(object sender, EventArgs e)
        {
            if(filter == false)
            {
                pageNumber++;
                grid.DataSource = GridSource;
                grid.Columns.RemoveAt(2);
                grid.Columns.RemoveAt(2);
                comboBox1Items(comboBox1, pageNumber);
            }
            else
            {
                filteredPageNumber++;
                grid.DataSource = GridSourceFiltered();
                grid.Columns.RemoveAt(2);
                grid.Columns.RemoveAt(2);
                comboBox1Items(comboBox1, filteredPageNumber);
            }
        }

        private void ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            pageNumber = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            grid.DataSource = GridSource;
            grid.Columns.RemoveAt(2);
            grid.Columns.RemoveAt(2);
            comboBox1Items(comboBox1, pageNumber);
        }
        
        private void Grid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            using (var db = new nesneProjeEntities())
            {
                ArticleGridItem item = (ArticleGridItem)grid.Rows[e.RowIndex].DataBoundItem;

                label1.Visible = true;
                richTextBox1.Visible = true;

                List<ArticleGridItem> titles = db.Articles.Where(c => c.Title == item.Title)
                    .Select(c => new ArticleGridItem { Title = c.Title, FullContent = c.FullContent }).ToList();
                foreach(var title in titles)
                {
                    label1.Text = title.Title;
                    richTextBox1.Text = title.FullContent;
                }
            }
        }


        //----------------Methods-------------\\
        static void comboBox1Items(ComboBox comboBox1, int pageNumber)
        {
            comboBox1.Items.Clear();
            for(int i = pageNumber+1; i <= pageNumber + 5; i++)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.Text = pageNumber.ToString();
        }

        //GiveSource method
        IEnumerable<ArticleGridItem> GridSource
        {
            get
            {
                using (var db = new nesneProjeEntities())
                {
                    IEnumerable<ArticleGridItem> articles = db.Articles
                        .Select(c => new ArticleGridItem { Title = c.Title, Summary = c.ContentSummary }).ToList();
                    IEnumerable<ArticleGridItem> result = articles.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    return result;
                }
            }
        }

        IEnumerable<ArticleGridItem> GridSourceFiltered()
        {
            using(var db = new nesneProjeEntities())
            {

                ComboBoxItem boxItem = (ComboBoxItem)comboBox.SelectedItem;
                int count = db.Articles.ToList().Count();
                if(comboBox.Text == "Select Category")
                {
                    MessageBox.Show("Select Category First", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    Environment.Exit(0);
                    return null;
                }
                else if(boxItem.Text == "All")
                {
                    return GridSource;
                }
                else
                {
                    int category = db.ArticleCategories.Where(c => c.CategoryName == boxItem.Text).Select(c => c.ID).First();
                    List<ArticleGridItem> articleList = new List<ArticleGridItem>();

                    List<int> relation = db.ArticleCategoryRelations.Where(a => a.CategoryID == category).Select(c => c.ArticleID).ToList();

                    foreach (int rel in relation)
                    {
                        List<ArticleGridItem> articles = db.Articles.Where(c => c.ID == rel).
                            Select(c => new ArticleGridItem { Title = c.Title, Summary = c.ContentSummary }).ToList();

                        foreach (var article in articles)
                        {
                            articleList.Add(article);
                        }
                    }
                    IEnumerable<ArticleGridItem> articles1 = articleList;
                    IEnumerable<ArticleGridItem> result = articles1.Skip((filteredPageNumber - 1) * filteredPageSize).Take(filteredPageSize).ToList();
                    filter = true;
                    return result;
                }

            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void PdfButton_Click(object sender, EventArgs e)
        {
            using(var db = new nesneProjeEntities())
            {
                if(comboBox.Text == "Select Category")
                {
                    MessageBox.Show("Select Category First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }

                FileStream fs = new FileStream("Articles.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
                Document doc = new Document(PageSize.A4, 7f, 5f, 5f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                iTextSharp.text.Font mainFont = FontFactory
                    .GetFont("Segoe UI", 14, new iTextSharp.text.BaseColor(System.Drawing.ColorTranslator.FromHtml("#999")));

                ComboBoxItem item = (ComboBoxItem)comboBox.SelectedItem;
                int category = db.ArticleCategories.Where(c => c.ID == item.ID).Select(c => c.ID).First();
                List<int> relation = db.ArticleCategoryRelations.Where(c => c.CategoryID == category).Select(c => c.ArticleID).ToList();

                doc.Open();

                var font = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

                foreach (int rel in relation)
                {
                    List<ArticleGridItem> articles = db.Articles.Where(c => c.ID == rel)
                        .Select(c => new ArticleGridItem { Title = c.Title, FullContent = c.FullContent }).ToList();
                    foreach(var article in articles)
                    {
                        Chunk c = new Chunk(article.Title, boldFont);
                        doc.Add(new Paragraph(c));
                        Chunk c2 = new Chunk(article.FullContent, font);
                        doc.Add(new Paragraph(c2));
                        doc.Add(new Paragraph("\n\n"));
                    }
                }
                doc.Close();
                fs.Close();
            }
        }
    }
}
