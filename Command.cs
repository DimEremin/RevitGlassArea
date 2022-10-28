#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


#endregion

namespace RevitAddin3
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;



            UserControl1 mainWindow = new UserControl1(doc);
            mainWindow.ShowDialog();




            return Result.Succeeded;
        }




    }
    public static class Calc
    {
        public static Document Doc;
        public static double GlassArea(Document doc)
        {
            Doc = doc;

            double area = 0;

            //� ������� ����� ������ ���������� ������� �� ���� ������ ��� ������ ������ ��� ���������
            FilteredElementCollector glassFilter = new FilteredElementCollector(Doc);
            glassFilter.OfCategory(BuiltInCategory.OST_Windows)
                .OfClass(typeof(FamilyInstance));
            foreach (FamilyInstance window in glassFilter)
            {
                area += GetArea(window);
            }
            return area;
        }

        //����� ��� ������� ������� ���������� ������ ����
        public static double GetArea (FamilyInstance window)
        {
            double area = 0;
            Options option = new Options();
            option.ComputeReferences = true;

            //�������� �������� ��������� ���������� ����
            GeometryInstance geometry = window.get_Geometry(option).Cast<GeometryInstance>()
                        .FirstOrDefault();

            foreach (var g in geometry.GetInstanceGeometry()) 
            {
                GraphicsStyle gStyle = Doc.GetElement(g.GraphicsStyleId) as GraphicsStyle;

                if (gStyle != null)
                {
                    if (gStyle.GraphicsStyleCategory.Name == "����������")
                    {
                        //�������� ������� ���� Solid ���� ����� ��������������� ������� - ����������
                        Solid solid = g as Solid;
                        if (solid != null)
                        {
                           if (solid.SurfaceArea > 0)
                            {
                                //���������� ������ �������, ������������ �������� ������ ������
                                area += (solid.SurfaceArea / 2.00);
                            }
                        }

                    }

                }
            }




            return area;
        }

    }
}
