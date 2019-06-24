using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace WordingLink
{
    public class ModelXmlHelper
    {
        string path = string.Empty;
        XDocument xmlDoc = null;

        public ModelXmlHelper()
        {
            path = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), CONST.FILEPATH_MODELINFO);
            if (File.Exists(path))
            {
                xmlDoc = XDocument.Load(path);
            }
        }

        public bool GetJPN(string modelName, string modelType,ref string firstJPN, ref string lastJPN)
        {
            bool getSuccess = false;
            firstJPN = string.Empty;
            lastJPN = string.Empty;
            try
            {
                if (xmlDoc != null)
                {
                    XElement rootNode = xmlDoc.Element("ModelInfo");

                    IEnumerable<XElement> NameNodes =   from myTarget in rootNode.Descendants("Series")
                                                        where myTarget.Attribute("Name").Value.Equals(modelName)
                                                        && myTarget.HasElements
                                                        select myTarget;

                    if (NameNodes.Count() > 0)
                    {
                        IEnumerable<XElement>  TypeNodes =  from myTarget in NameNodes.First().Descendants("Model")
                                                            where myTarget.Attribute("Name").Value.Equals(modelType)
                                                            && myTarget.HasElements
                                                            select myTarget;
                        if (TypeNodes.Count() > 0)
                        {
                            if (TypeNodes.First().Element("FirstJPN") != null)
                            {
                                firstJPN = TypeNodes.First().Element("FirstJPN").Value;
                            }
                            getSuccess = true;
                            if (TypeNodes.First().Element("LastJPN") != null)
                            {
                                lastJPN = TypeNodes.First().Element("LastJPN").Value;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return getSuccess;
        }

        public bool GetFilePath(string modelName, string modelType, ref string linkPath, ref string hmiPath)
        {
            bool getSuccess = false;
            linkPath = string.Empty;
            hmiPath = string.Empty;
            try
            {
                if (xmlDoc != null)
                {
                    XElement rootNode = xmlDoc.Element("ModelInfo");
                    IEnumerable<XElement> NameNodes =   from myTarget in rootNode.Descendants("Series")
                                                        where myTarget.Attribute("Name").Value.Equals(modelName)
                                                        && myTarget.HasElements
                                                        select myTarget;

                    if (NameNodes.Count() > 0)
                    {
                        IEnumerable<XElement>  TypeNodes =  from myTarget in NameNodes.First().Descendants("Model")
                                                            where myTarget.Attribute("Name").Value.Equals(modelType)
                                                            && myTarget.HasElements
                                                            select myTarget;
                        if (TypeNodes.Count() > 0)
                        {
                            linkPath = TypeNodes.First().Element("LinkFilePath").Value;
                            hmiPath = TypeNodes.First().Element("HmiStridPath").Value;
                            getSuccess = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return getSuccess;
        }

        public bool GetByFirstJPN(string firstJPN, ref Dictionary<String, ArrayList> listModel)
        {
            bool getSuccess = false;
            IEnumerable<XElement> myTargetNodes = null;
            try
            {
                if (xmlDoc != null)
                {
                    XElement rootNode = xmlDoc.Element("ModelInfo");
                    foreach (XElement NameNode in rootNode.Elements())
                    {
                        myTargetNodes = from myTarget in NameNode.Descendants("Model")
                                        where (myTarget.Element("FirstJPN") != null)
                                        && myTarget.Element("FirstJPN").Value.Equals(firstJPN)
                                        && myTarget.HasElements
                                        select myTarget;
                        foreach(XElement TypeNode in myTargetNodes)
                        {
                            string modelName = NameNode.Attribute("Name").Value;
                            string modelType = TypeNode.Attribute("Name").Value;
                            if (listModel.Keys.Contains(modelName))
                            {
                                listModel[modelName].Add(modelType);
                            }
                            else
                            {
                                listModel.Add(modelName, new ArrayList() { modelType });
                            }
                        }
                    }
                    getSuccess = true;
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return getSuccess;
        }

        public bool GetAllJPN(ref List<string> listModel)
        {
            bool getSuccess = false;
            try
            {
                if (xmlDoc != null)
                {
                    XElement rootNode = xmlDoc.Element("ModelInfo");
                    foreach (XElement NameNode in rootNode.Elements())
                    {
                        foreach (XElement modelType in NameNode.Elements())
                        {
                            
                            if ((modelType.Element("FirstJPN") != null) &&
                                (modelType.Element("FirstJPN").Value != string.Empty) &&
                                !listModel.Contains(modelType.Element("FirstJPN").Value))
                            {
                                listModel.Add(modelType.Element("FirstJPN").Value);
                            }

                            if ((modelType.Element("LastJPN") != null) &&
                                (modelType.Element("LastJPN").Value != string.Empty) &&
                                !listModel.Contains(modelType.Element("LastJPN").Value))
                            {
                                listModel.Add(modelType.Element("LastJPN").Value);
                            }
                        }
                    }
                    getSuccess = true;
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return getSuccess;
        }

        public bool SetFilePath(string modelName, string modelType, string linkPath, string hmiPath)
        {
            bool setSuccess = false;
            try
            {
                if (xmlDoc != null)
                {
                    XElement rootNode = xmlDoc.Element("ModelInfo");
                    IEnumerable<XElement> NameNodes = from myTarget in rootNode.Descendants("Series")
                                                      where myTarget.Attribute("Name").Value.Equals(modelName)
                                                      && myTarget.HasElements
                                                      select myTarget;
                    if (NameNodes.Count() > 0)
                    {
                        IEnumerable<XElement> TypeNodes = from myTarget in NameNodes.First().Descendants("Model")
                                                          where myTarget.Attribute("Name").Value.Equals(modelType)
                                                          && myTarget.HasElements
                                                          select myTarget;
                        if (TypeNodes.Count() > 0)
                        {
                            TypeNodes.First().Element("LinkFilePath").SetValue(linkPath);
                            TypeNodes.First().Element("HmiStridPath").SetValue(hmiPath);
                            rootNode.Save(path);
                            setSuccess = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return setSuccess;
        }
    }
}
