/*
 * File Name: ScreenInfo.cs
 * Creat Date: 2017-3-24
 * Description: Screen Info Class
 * Modify Content:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tool.BLL
{
    enum ElementShipType
    {
        StringOnly,
        StringWithImage,
        ImageOnly,
        ImageWithString
    }
    /*
     * Description: Screen Info Class for Abstract the Whole Screen,include Button Info List and String Info List.
     */
    class Screen
    {
        [JsonProperty]
        public static Size screenSize = null;
        [JsonProperty]
        private ScreenIdentify screenIdentify;
        [JsonProperty]
        private List<AbstractElement> elementList;
        //Use For Save&Process the Relationship of ElementImage and ElementString
        [JsonProperty]
        private Dictionary<AbstractElement, List<AbstractElement>> elementShip;
        [JsonProperty]
        private List<AbstractControl> controlList;

        /* 
         *  Description:special constructor
         *  Param:btnInfoList - the Button Info List of Screen Info
         *        strInfoList - the String Info List of Screen Info
         *  Return:
         *  Exception:
         *  Example:ScreenInfo(btnInfoList)
         *          ScreenInfo(btnInfoList,strInfoList)
         */
        public Screen()
        {
            this.screenIdentify = new ScreenIdentify();

            this.elementList = new List<AbstractElement>();

            this.elementShip = new Dictionary<AbstractElement, List<AbstractElement>>();

            this.controlList = new List<AbstractControl>();

            if (null == screenSize)
                throw new FTBAutoTestException("Create screen error by null Static Screen Size.");
        }

        //ScreenIdentify Operation
        public void setIdentifyScreenId(string id)
        {
            screenIdentify.scrId = id;
        }
        public void fixIdentify()
        {
            //get String List
            List<AbstractElement> strList = this.getElementList(typeof(ElementString));
            if (null != strList)
            {
                screenIdentify.btnWordsVector = new List<string>();
                foreach (ElementString eleStr in strList)
                {
                    if ("" != eleStr.str)
                    {
                        screenIdentify.btnWordsVector.Add(eleStr.str);
                    }
                }
            }

            //get Title
            List<AbstractControl> titleList = this.getControlList(typeof(ControlTitle));
            if (null != titleList)
            {
                ControlTitle title = (ControlTitle)titleList[0];
                screenIdentify.scrTitle = title.str.str;
            }
        }
        public ScreenIdentify getIdentify()
        {
            //return screen Identification
            if (screenIdentify.isNull())
                fixIdentify();
            return screenIdentify;
        }

        //Element Operation
        public void addElement(AbstractElement element)
        {
            //todo check contains method
            if (!elementList.Contains(element))
                elementList.Add(element);
        }
        public AbstractElement getElement(ulong elementId)
        {
            foreach (AbstractElement element in elementList)
            {
                if (elementId == element.id)
                {
                    return element;
                }
            }

            return null;
        }
        public AbstractElement findElement(AbstractElement targetElement)
        {
            foreach (AbstractElement element in elementList)
                if (element.EqualsBySingleAttribute(targetElement))
                    return element;
            return null;
        }
        public void removeElement(AbstractElement targetElement)
        {
            if (!elementList.Contains(targetElement))
                throw new FTBAutoTestException("Remove element error, targetElement is not Contains.");
            elementList.Remove(targetElement);
        }
        public List<AbstractElement> getElementList()
        {
            return elementList;
        }
        public List<AbstractElement> getElementList(Type elementType,
                                                    bool hasNotFixed = false)
        {
            List<AbstractElement> tmpElementList = new List<AbstractElement>();

            foreach (AbstractElement element in elementList)
            {
                if (elementType == element.GetType())
                {
                    if (false == hasNotFixed)
                        tmpElementList.Add(element);
                    else if (false == element.isFixed())
                        tmpElementList.Add(element);
                }
            }

            if (0 == tmpElementList.Count)
                return null;
            else
                return tmpElementList;
        }
        public bool isAllElementFixded()
        {
            foreach (AbstractElement ele in elementList)
            {
                if (false == ele.isFixed())
                    return false;
            }
            return true;
        }

        //Element Relationship Operation
        public void addElementShip(AbstractElement key,
                                   List<AbstractElement> value)
        {
            if (null == key || !elementList.Contains(key))
                throw new FTBAutoTestException("Add elementship error by unknow key.");
            if (null != value)
                foreach (AbstractElement ele in value)
                    if (!elementList.Contains(ele))
                        throw new FTBAutoTestException("Add elementship error by unknow value.");
            elementShip.Add(key, value);
        }
        public void removeElementShip(AbstractElement targetKey)
        {
            if (!elementShip.ContainsKey(targetKey))
                throw new FTBAutoTestException("Remove elementShip error, targetElement is not Contains.");
            foreach (KeyValuePair<AbstractElement, List<AbstractElement>> ship in elementShip)
            {
                if (ship.Key == targetKey)
                {
                    elementShip.Remove(targetKey);
                    break;
                }
                //if (ship.Value != null && ship.Value.Contains(targetKey))
                //{
                //    ship.Value.Remove(targetKey);
                //}
            }
        }
        public List<AbstractElement> getElementShipKeyList(ElementShipType shipType,
                                                           bool hasNotFixed = false)
        {
            List<AbstractElement> targetElementList = new List<AbstractElement>();
            foreach (KeyValuePair<AbstractElement, List<AbstractElement>> ship in elementShip)
            {
                AbstractElement targetElement = null;
                switch (shipType)
                {
                    case ElementShipType.ImageOnly:
                        if (ship.Key is ElementImage && null == ship.Value)
                            targetElement = ship.Key;
                        break;
                    case ElementShipType.ImageWithString:
                        if (ship.Key is ElementImage && null != ship.Value)
                            targetElement = ship.Key;
                        break;
                    case ElementShipType.StringOnly:
                        if (ship.Key is ElementString && null == ship.Value)
                            targetElement = ship.Key;
                        break;
                    case ElementShipType.StringWithImage:
                        if (ship.Key is ElementString && null != ship.Value)
                            targetElement = ship.Key;
                        break;
                    default:
                        //won't run to here
                        break;
                }
                if (null != targetElement)
                {
                    if (false == hasNotFixed)
                        targetElementList.Add(targetElement);
                    else if (false == targetElement.isFixed())
                        targetElementList.Add(targetElement);
                }
            }

            if (0 == targetElementList.Count)
                return null;
            else
                return targetElementList;
        }
        public List<AbstractElement> getElementShipValueList(AbstractElement key)
        {
            if (null == key)
                throw new FTBAutoTestException("Get elementShip error by empty key.");

            if (elementShip.ContainsKey(key))
                return elementShip[key];
            else
                return null;
        }

        //Control Operation
        public void addControl(AbstractControl control)
        {
            //todo check contains method
            if (!controlList.Contains(control))
                controlList.Add(control);
        }
        public List<AbstractControl> getControlList()
        {
            return controlList;
        }
        public List<AbstractControl> getControlList(Type controlType)
        {
            List<AbstractControl> tmpControlList = new List<AbstractControl>();

            foreach (AbstractControl control in controlList)
            {
                if (controlType == control.GetType())
                {
                    tmpControlList.Add(control);
                }
                else if(control.GetType().BaseType.Name == controlType.Name)
                {
                    tmpControlList.Add(control);
                }
            }

            if (0 == tmpControlList.Count)
                return null;
            else
                return tmpControlList;
        }
        /* todo
         *  Description:to get the Button Info by given Button Identification
         *  Param:targetBtnIdentify - Target Button's Identification
         *  Return:targetBtnInfo - if found
         *         null - if not found
         *  Exception:
         *  Example:ButtonInfo runtimeButton = runtimeScreenInfo.get(ftbButtonIdentify);
         *          if (null == Button)
         *              ;//failed
         *          else
         *              ;//the Operation of Button Info,Such as Click
         */
        public ControlButton findButton(ControlButtonIdentify targetBtnIdentify)
        {
            foreach (AbstractControl control in controlList)
            {
                ControlButton button = control as ControlButton;
                if (null == button)
                    continue;
                if (true == button.getIdentify().Equals(targetBtnIdentify))
                    return button;
            }
            return null;
        }
        public bool isScrollable()
        {
            foreach (AbstractControl control in controlList)
            {
                if (control is IAggregateCreateAPI)
                    return true;
            }
            return false;
        }
        public AbstractControl getScrollControl()
        {
            foreach (AbstractControl control in controlList)
            {
                if (control is IAggregateCreateAPI)
                    return control;
            }
            return null;
        }
        public bool isInside(Rectangle rect)
        {
            if (rect.x >= 0 && rect.y >= 0 && rect.x + rect.w <= screenSize.w && rect.y + rect.h <= screenSize.h)
                return true;
            return false;
        }

        //Display ScreenInfo
        public string listElement()
        {
            string str = "ScreenElement={";
            string tmpStr = "";
            foreach (AbstractElement ele in elementList)
                tmpStr += "\r\n" + ele.ToString();
            str += tmpStr.Replace("\r\n", "\r\n\t");
            str += "\r\n};\r\n";
            return str;
        }
        public string listElementShip()
        {
            string str = "ScreenElementShip={\r\n";
            string tmpStr = "";
            foreach (KeyValuePair<AbstractElement, List<AbstractElement>> ship in elementShip)
            {
                string[] tmpStrArray = ship.Key.ToString().Split(',');
                tmpStr += "[" + tmpStrArray[0] + "," + tmpStrArray[1] + "] => ";

                if (null == ship.Value)
                    tmpStr += "null;\r\n";
                else
                {
                    foreach (AbstractElement ele in ship.Value)
                    {
                        tmpStrArray = ele.ToString().Split(',');
                        tmpStr += "[" + tmpStrArray[0] + "," + tmpStrArray[1] + "],";
                    }
                    tmpStr = tmpStr.Remove(tmpStr.Count() - 1);
                    tmpStr += ";\r\n";
                }
            }
            str += tmpStr;
            str += "}\r\n";
            return str;
        }
        public string listControl()
        {
            string str = "ScreenControl={";
            string tmpStr = "";
            foreach (AbstractControl ctl in controlList)
            {
                tmpStr += "\r\n" + ctl.ToString();
            }
            str += tmpStr.Replace("\r\n", "\r\n\t");
            str += "\r\n};\r\n";
            return str;
        }

        //Equals method
        public bool EqualsById(Screen targetScreen)
        {
            if (null == targetScreen)
                return false;

            return getIdentify().Equals(targetScreen.getIdentify());
        }
        public bool EqualsByStringList(Screen targetScreen)
        {
            if (null == targetScreen)
                return false;
            List<string> currentStringList = getIdentify().btnWordsVector;
            List<string> stringList = targetScreen.getIdentify().btnWordsVector;
            if (null == stringList || null == currentStringList)
                throw new FTBAutoTestException("Check screen equals error, cause empty btnWordsVector get.");
            if (currentStringList.Count != stringList.Count)
                return false;
            if (0 != currentStringList.Where((a, i) => a != stringList[i]).Count())
                return false;
            return true;
        }
        public bool EqualsByElementList(Screen targetScreen)
        {
            List<AbstractElement> elementList = targetScreen.getElementList();
            if (null == elementList)
                throw new FTBAutoTestException("Check screen equals error, cause empty elementList get.");
            if (this.elementList.Count != elementList.Count)
                return false;
            int tmp = this.elementList.Where((a, i) => false == a.EqualsByAllAttribute(elementList[i])).Count();
            if (0 != tmp)
                return false;
            return true;
        }
        public bool EqualsByControlList(Screen targetScreen)
        {
            List<AbstractControl> controlList = targetScreen.getControlList();
            if (null == controlList)
                throw new FTBAutoTestException("Check screen equals error, cause empty elementList get.");
            if (this.controlList.Count != controlList.Count)
                return false;
            if (0 != this.controlList.Where((a, i) => false == a.Equals(controlList[i])).Count())
                return false;
            return true;
        }
    }
}
