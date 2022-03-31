using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.Utils
{
    public class TreeViewUtils
    {
        public static void LoadTree(IList<BackEnd.Domain.Group> items, TreeNodeCollection nodes)
        {
            foreach (BackEnd.Domain.Group item in items)
            {
                TreeNode node = GetTreeNode(item);
                nodes.Add(node);
                LoadTree(item.Conditions, node.ChildNodes);
            }
        }

        public static void LoadTree(IList<GroupCondition> items, TreeNodeCollection nodes)
        {
            foreach (GroupCondition item in items)
            {
                TreeNode node = GetTreeNode(item);
                nodes.Add(node);
            }
        }

        private static TreeNode GetTreeNode(BackEnd.Domain.Group item)
        {
            var node = new TreeNode
                           {
                               Text = item.Name,
                               Value = Convert.ToString(item.Id),
                               ToolTip = item.Name,
                               Expanded = true
                           };
            return node;
        }

        private static TreeNode GetTreeNode(GroupCondition item)
        {
            var node = new TreeNode
                           {
                               Text = string.Format("{0} - {1}", item.ConditionTypeText, item.VisibleValue),
                               Value = Convert.ToString(item.Id),
                               ToolTip = string.Format("{0} - {1}", item.ConditionTypeText, item.VisibleValue),
                               Expanded = true
                           };

            return node;
        }

        public static void SelectItems(IList<BackEnd.Domain.Group> items, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                foreach (BackEnd.Domain.Group group in items)
                {
                    if (node.Value == group.Id.ToString(CultureInfo.InvariantCulture))
                    {
                        node.Checked = true;
                    }
                }
            }
        }

        public static void SelectItems(IList<Group_R_Template> items, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                foreach (var groupRTemplate in items)
                {
                    if (node.Value == groupRTemplate.IdGroup.ToString(CultureInfo.InvariantCulture))
                    {
                        node.Checked = true;
                    }
                }
            }
        }

        public static void SelectItems(IList<Attachment_R_Group> items, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                foreach (var attachmentRGroup in items)
                {
                    if (node.Value == attachmentRGroup.IdGroup.ToString(CultureInfo.InvariantCulture))
                    {
                        node.Checked = true;
                    }
                }
            }
        }
    }
}