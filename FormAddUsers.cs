using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Practica_13.FolderModel;

namespace Practica_13
{
    public partial class FormAddUsers : Form
    {
        public FormAddUsers()
        {
            InitializeComponent();
        }

        ModelEF model = new ModelEF();

        private void FormAddUsers_Load(object sender, EventArgs e)
        {
            BindingSourceRole.DataSource = model.Roles.ToList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$",
                RegexOptions.IgnoreCase);
            if (!reg.IsMatch(textBoxEmail.Text))
            {
                MessageBox.Show("Михайлов АРТЕМ:Почта не соответствует требованиям!");
                return;
            }

            if (!textBoxPassword.Text.Equals(textBoxPassword2.Text))
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            if (String.IsNullOrWhiteSpace(textBoxLogin.Text) ||
                String.IsNullOrWhiteSpace(textBoxPassword.Text) ||
                String.IsNullOrWhiteSpace(textBoxName_First.Text) ||
                String.IsNullOrWhiteSpace(textBoxName_Second.Text) ||
                !maskedTextBoxPhone.MaskCompleted)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            Users users = new Users();
            users.ID = 0;
            users.Login = textBoxLogin.Text;
            users.Password = textBoxPassword.Text;
            users.Email = textBoxEmail.Text;
            users.Phone = maskedTextBoxPhone.Text;
            users.First_Name = textBoxName_First.Text;
            users.Second_Name = textBoxName_Second.Text;
            users.RoleID = (int)comboBoxRole.SelectedValue;
            users.Gender = radioButtonMen.Checked ? "Мужской" : "Женский";

            try
            {            
                model.Users.Add(users);
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Данные добавленны!");
            Close();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
