﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Geradorador_de_Senha
{
    public partial class frmgeradorSenha : Form
    {

        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);



        public frmgeradorSenha()
        {
            InitializeComponent();
        }

        private void frmgeradorSenha_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
        }

        public string GerarSenhas()
        {
            int Tamanho = 22; // Numero de digitos da senha
            string senha = string.Empty;
            for (int i = 0; i < Tamanho; i++)
            {
                Random random = new Random();
                int codigo = Convert.ToInt32(random.Next(31, 122).ToString());

                if ((codigo >= 48 && codigo <= 57) || (codigo >= 97 && codigo <= 122))
                {
                    string _char = ((char)codigo).ToString();
                    if (!senha.Contains(_char))
                    {
                        senha += _char;
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    i--;
                }
            }
            return senha;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtSenha.Clear();
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            txtSenha.SelectAll();
            txtSenha.Copy();
        }

        private void btngerarSenha_Click_1(object sender, EventArgs e)
        {
            string senha = GerarSenhas();
            txtSenha.Text = senha;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            string message = "Quer realmente fechar o Gerador de Senha ?";
            string title = "Aviso";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo; DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes) 
            { 
            MessageBox.Show("Tudo bem, programa está sendo encerrado");
            Application.Exit();
            }
            else
            {
                MessageBox.Show("Fechamento de janela cancelado");
            }

        }
    }
}