using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using CallplusUtil.Forms;

namespace CallplusUtil.Extensions
{
    public static class ComboBoxExtension
    {
        private const string Selecione = "SELECIONE...";
        private const string Todos = "TODOS";

        public static void Preencher<TEntity, TProperty>(this ComboBox comboBox, IEnumerable<TEntity> itens,
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

            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new MethodInvoker(() =>
                {
                    comboBox.DataSource = null;
                    comboBox.DataSource = bindingList;
                    comboBox.DisplayMember = "Text";
                    comboBox.ValueMember = "Value";

                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                }));
            }
            else
            {
                comboBox.DataSource = null;
                comboBox.DataSource = bindingList;
                comboBox.DisplayMember = "Text";
                comboBox.ValueMember = "Value";
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        public static void Preencher<TTipoKey, TTipoValue>(this ComboBox comboBox, IEnumerable<KeyValuePair<TTipoKey, TTipoValue>> itens)
        {
            BindingList<ListItem> bindingList = new BindingList<ListItem>();

            foreach (var item in itens)
            {
                bindingList.Add(new ListItem
                {
                    Value = item.Key.ToString(),
                    Text = item.Value.ToString()
                });

            }

            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new MethodInvoker(() =>
                {
                    comboBox.DataSource = null;
                    comboBox.DataSource = bindingList;
                    comboBox.DisplayMember = "Text";
                    comboBox.ValueMember = "Value";

                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                }));
            }
            else
            {
                comboBox.DataSource = null;
                comboBox.DataSource = bindingList;
                comboBox.DisplayMember = "Text";
                comboBox.ValueMember = "Value";

                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        public static void PreencherComSelecione<TTipoKey, TTipoValue>(this ComboBox comboBox, IEnumerable<KeyValuePair<TTipoKey, TTipoValue>> itens)
        {
            BindingList<ListItem> bindingList = new BindingList<ListItem>();

            bindingList.Add(new ListItem { Value = "-1", Text = Selecione });
            foreach (var item in itens)
            {
                bindingList.Add(new ListItem
                {
                    Value = item.Key.ToString(),
                    Text = item.Value.ToString()
                });

            }

            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new MethodInvoker(() =>
                {
                    comboBox.DataSource = null;
                    comboBox.DataSource = bindingList;
                    comboBox.DisplayMember = "Text";
                    comboBox.ValueMember = "Value";

                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                }));
            }
            else
            {
                comboBox.DataSource = null;
                comboBox.DataSource = bindingList;
                comboBox.DisplayMember = "Text";
                comboBox.ValueMember = "Value";

                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }


        }

        public static void PreencherComSelecione<TEntity, TProperty>(this ComboBox comboBox, IEnumerable<TEntity> itens,
            Expression<Func<TEntity, TProperty>> valueMember, Expression<Func<TEntity, string>> displayMember)
        {
            BindingList<ListItem> bindingList = new BindingList<ListItem>();

            bindingList.Add(new ListItem { Value = "-1", Text = Selecione });

            foreach (var item in itens)
            {
                bindingList.Add(new ListItem
                {
                    Value = valueMember.Compile().Invoke(item).ToString(),
                    Text = displayMember.Compile().Invoke(item)
                });

            }

            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new MethodInvoker(() =>
                {
                    comboBox.DataSource = null;
                    comboBox.DataSource = bindingList;
                    comboBox.DisplayMember = "Text";
                    comboBox.ValueMember = "Value";

                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                }));
            }
            else
            {
                comboBox.DataSource = null;
                comboBox.DataSource = bindingList;
                comboBox.DisplayMember = "Text";
                comboBox.ValueMember = "Value";
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        public static void PreencherComTodos<TEntity, TProperty>(this ComboBox comboBox, IEnumerable<TEntity> itens,
            Expression<Func<TEntity, TProperty>> valueMember, Expression<Func<TEntity, string>> displayMember)
        {
            BindingList<ListItem> bindingList = new BindingList<ListItem>();

            bindingList.Add(new ListItem { Value = "-1", Text = Todos });
            foreach (var item in itens)
            {
                bindingList.Add(new ListItem
                {
                    Value = valueMember.Compile().Invoke(item).ToString(),
                    Text = displayMember.Compile().Invoke(item)
                });

            }
            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new MethodInvoker(() =>
                {
                    comboBox.DataSource = null;
                    comboBox.DataSource = bindingList;
                    comboBox.DisplayMember = "Text";
                    comboBox.ValueMember = "Value";

                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                }));
            }
            else
            {
                comboBox.DataSource = null;
                comboBox.DataSource = bindingList;
                comboBox.DisplayMember = "Text";
                comboBox.ValueMember = "Value";
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }

        }

        public static void PreencherComTodosESelecione<TEntity, TProperty>(this ComboBox comboBox, IEnumerable<TEntity> itens,
    Expression<Func<TEntity, TProperty>> valueMember, Expression<Func<TEntity, string>> displayMember)
        {
            BindingList<ListItem> bindingList = new BindingList<ListItem>();

            bindingList.Add(new ListItem { Value = "-2", Text = Todos });
            //bindingList.Add(new ListItem { Value = "-1", Text = Selecione });
            foreach (var item in itens)
            {
                bindingList.Add(new ListItem
                {
                    Value = valueMember.Compile().Invoke(item).ToString(),
                    Text = displayMember.Compile().Invoke(item)
                });

            }
            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new MethodInvoker(() =>
                {
                    comboBox.DataSource = null;
                    comboBox.DataSource = bindingList;
                    comboBox.DisplayMember = "Text";
                    comboBox.ValueMember = "Value";

                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                }));
            }
            else
            {
                comboBox.DataSource = null;
                comboBox.DataSource = bindingList;
                comboBox.DisplayMember = "Text";
                comboBox.ValueMember = "Value";
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        public static void DefinirComoSelecione(this ComboBox comboBox)
        {
            comboBox.DataSource = null;
            BindingList<KeyValuePair<int, string>> bindingList = new BindingList<KeyValuePair<int, string>>();
            bindingList.Add(new KeyValuePair<int, string>(-1, Selecione));

            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new MethodInvoker(() =>
                {
                    comboBox.DataSource = bindingList;
                    comboBox.DisplayMember = "Value";
                    comboBox.ValueMember = "Key";
                }));
            }
            else
            {
                comboBox.DataSource = bindingList;
                comboBox.DisplayMember = "Value";
                comboBox.ValueMember = "Key";
            }


        }

        public static bool TextoEhSelecione(this ComboBox comboBox)
        {
            return comboBox.Text == Selecione;
        }

        public static bool TextoEhTodos(this ComboBox comboBox)
        {
            return comboBox.Text == Todos;
        }

        private static MemberInfo GetMemberInfo<TObject, TProperty>(Expression<Func<TObject, TProperty>> expression)
        {
            //if (expression.Body is MemberExpression member)
            //{
            //    return member.Member;
            //}
            throw new ArgumentException("Membro não existe.");
        }

        public static void ResetarComSelecione(this ComboBox comboBox, bool habilitar)
        {
            CallplusFormsUtil.ResetarComboComSelecione(comboBox, habilitar);
        }

        public static void ResetarComReticencias(this ComboBox comboBox, bool habilitar)
        {
            CallplusFormsUtil.ResetarComboComReticencias(comboBox, habilitar);
        }

        public static void Habilitar(this ComboBox comboBox)
        {
            CallplusFormsUtil.HabilitarComboBox(comboBox);
        }

        public static void Desabilitar(this ComboBox comboBox)
        {
            CallplusFormsUtil.DesabilitarComboBox(comboBox);
        }

        public static void SelecionarPrimeiroItemDisponivel(this ComboBox comboBox)
        {
            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new MethodInvoker(() =>
                {
                    var enumerator = comboBox.Items.GetEnumerator();

                    while (enumerator.MoveNext())
                    {
                        var current = enumerator.Current;

                        string txt = current.ToString();

                        if (txt == Selecione || txt == Todos)
                        {
                            continue;
                        }

                        comboBox.Text = txt;
                    }
                }));

            }
            else
            {
                var enumerator = comboBox.Items.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;

                    string txt = current.ToString();

                    if (txt == Selecione || txt == Todos)
                    {
                        continue;
                    }

                    comboBox.Text = txt;
                }
            }
        }

        public static void PreencherComTodos<TTipoKey, TTipoValue>(this ComboBox comboBox, IEnumerable<KeyValuePair<TTipoKey, TTipoValue>> itens)
        {
            BindingList<ListItem> bindingList = new BindingList<ListItem>();

            bindingList.Add(new ListItem { Value = "-1", Text = Todos });
            foreach (var item in itens)
            {
                bindingList.Add(new ListItem
                {
                    Value = item.Key.ToString(),
                    Text = item.Value.ToString()
                });

            }

            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new MethodInvoker(() =>
                {
                    comboBox.DataSource = null;
                    comboBox.DataSource = bindingList;
                    comboBox.DisplayMember = "Text";
                    comboBox.ValueMember = "Value";

                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                }));
            }
            else
            {
                comboBox.DataSource = null;
                comboBox.DataSource = bindingList;
                comboBox.DisplayMember = "Text";
                comboBox.ValueMember = "Value";

                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }


        }

        public static void DefinirComoTodos(this ComboBox comboBox)
        {
            comboBox.DataSource = null;
            BindingList<KeyValuePair<int, string>> bindingList = new BindingList<KeyValuePair<int, string>>();
            bindingList.Add(new KeyValuePair<int, string>(-1, Todos));

            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new MethodInvoker(() =>
                {
                    comboBox.DataSource = bindingList;
                    comboBox.DisplayMember = "Value";
                    comboBox.ValueMember = "Key";
                }));
            }
            else
            {
                comboBox.DataSource = bindingList;
                comboBox.DisplayMember = "Value";
                comboBox.ValueMember = "Key";
            }
        }
    }
}
