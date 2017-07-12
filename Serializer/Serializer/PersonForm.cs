using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serializer
{
    public partial class Persons : Form
    {
        public Persons()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Person person = new Person(nameTxt.Text, addressTxt.Text, phoneTxt.Text);
            person.Serialize();
        }

        private void Persons_Load(object sender, EventArgs e)
        {
            Person person = Person.ShowFirst();
            nameTxt.Text = person.Name;
            addressTxt.Text = person.Address;
            phoneTxt.Text = person.Phone;

        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            Person person = Person.ShowNext();
            nameTxt.Text = person.Name;
            addressTxt.Text = person.Address;
            phoneTxt.Text = person.Phone;
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            Person person = Person.ShowPrevious();
            nameTxt.Text = person.Name;
            addressTxt.Text = person.Address;
            phoneTxt.Text = person.Phone;
        }

        private void firstBtn_Click(object sender, EventArgs e)
        {
            Person person = Person.ShowFirst();
            nameTxt.Text = person.Name;
            addressTxt.Text = person.Address;
            phoneTxt.Text = person.Phone;
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            Person person = Person.ShowLast();
            nameTxt.Text = person.Name;
            addressTxt.Text = person.Address;
            phoneTxt.Text = person.Phone;
        }
    }
}
