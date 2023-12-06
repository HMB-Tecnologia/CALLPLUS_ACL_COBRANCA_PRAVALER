using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using CheckedListBox = System.Windows.Forms.CheckedListBox;

namespace CallplusUtil.Extensions
{
    public static class CheckedListBoxExtension
    {
        public static void SetarTodosRegistros(this CheckedListBox clb, bool check)
        {
            if (clb == null) return;

            for (int i = 0; i < clb.Items.Count; ++i)
                clb.SetItemChecked(i, check);
        }

        public static void Preencher<TEntity, TProperty>(this CheckedListBox checkedListBox, IEnumerable<TEntity> itens,
            Expression<Func<TEntity, TProperty>> valueMember, Expression<Func<TEntity, string>> displayMember)

        {
            BindingList<ListItem> bindingList = new BindingList<ListItem>();
            foreach (var item in itens)
            {
                bindingList.Add(new ListItem
                {
                    Value = valueMember.Compile().Invoke(item).ToString(),
                    Text = displayMember.Compile().Invoke(item)
                });

            }

            if (checkedListBox.InvokeRequired)
            {
                checkedListBox.BeginInvoke(new MethodInvoker(() =>
                {
                    checkedListBox.DataSource = null;
                    checkedListBox.DataSource = bindingList;
                    checkedListBox.DisplayMember = "Text";
                    checkedListBox.ValueMember = "Value";
                }));
            }
            else
            {
                checkedListBox.DataSource = null;
                checkedListBox.DataSource = bindingList;
                checkedListBox.DisplayMember = "Text";

                checkedListBox.ValueMember = "Value";
            }
        }


        public static bool PossuiItemSelcionado(this CheckedListBox checkedListBox)
        {
            return checkedListBox.CheckedItems.Count > 0;
        }

        
}
}
