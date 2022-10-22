using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayTable
{
    public partial class DisplayAuthorsTable : Form
    {
        public DisplayAuthorsTable()
        {
            InitializeComponent();
        }

        // Entity Framework Dbcontext
        private BooksExamples.BooksEntities dbcontext = new BooksExamples.BooksEntities();

        private void DisplayAuthorsTable_Load(object sender, EventArgs e)
        {
            // Load Authors table ordered by LastName then FirstName
            dbcontext.Authors
                .OrderBy(author => author.LastName)
                .ThenBy(author => author.FirstName)
                .Load();

            // Specify DataSource for authorBindingSource
            authorBindingSource.DataSource = dbcontext.Authors.Local;
        }

        // Click event handler for the Save Button in the
        // BindingNavigator saves the changes made to the data
        private void authorBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Validate the input fields
            Validate();
            // Complete current edit, if any
            authorBindingSource.EndEdit();

            try
            {
                dbcontext.SaveChanges();
            }
            catch(DbEntityValidationException)
            {
                MessageBox.Show("FirstName and LastName must contain values", "Entity Validation Exception");
            }
        }

        // 
    }
}
