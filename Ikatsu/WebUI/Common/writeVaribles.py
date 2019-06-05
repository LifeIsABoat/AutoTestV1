# -*- coding: utf-8 -*-
import  xdrlib,sys
import  xlrd
import  os
reload(sys)
sys.setdefaultencoding('utf-8')

def open_excel(file):
    try:
        data = xlrd.open_workbook(file)
        return data
    except Exception,e:
        print str(e)


#根据名称获取Excel表格中的数据   参数:file：Excel文件路径     colnameindex：表头列名所在行的所以  ，by_name：Sheet1名称
def excel_table_byname(file= 'ClearExcelTest.xlsx',colnameindex=0,by_name=u'EWS_General_FAX_Copy_Print'):
    #creatPath = os.path.split(os.path.realpath(__file__))[0]
    valstr = '*** Variables ***' + '\n'
    num = 1
    tab = ' ' * 4
    data = open_excel(file)
    table = data.sheet_by_name(by_name)
    nrows = table.nrows #行数
    ncols = table.ncols #列数
    with open('TestOptionValue.txt','w') as f:
            f.write(valstr)

            for nrow in range(0, nrows):
                laist =[]
                print table.cell(nrow, ncols-1).value
                # if nrow == 0:
                #     continue

                for i in range(1, ncols-1):
                    if table.cell(nrow, i).value == '':
                        continue
                    uid = table.cell(nrow, i).value#取值..第nrow行第i列
                    laist.append(uid)
                f.write('${CaseNo.%d}'%(num) + tab + table.cell(nrow, 0).value + '\n')
                f.write('@{Test_Path%d}'%(num) + tab + tab.join(laist) + '\n')
                #f.write('@{Option_Index%d}'%(num) + tab + '0' + '\n')
                vvaelu = table.cell(nrow, ncols-1).value
                if type(vvaelu)==float:
                    tempstr = '${Option_Value%d}'%(num) + tab + str(float(vvaelu))
                    tempstr = tempstr.replace('.0','')
                    #valstr = (tempstr + '\n')
                    f.write(tempstr + '\n')
                elif type(vvaelu)==int:
                    #valstr = ('${Option_Value%d}'%(num) + tab + bytes(vvaelu) + '\n')
                    f.write('${Option_Value%d}'%(num) + tab + bytes(vvaelu) + '\n')
                else:
                    #valstr = ('${Option_Value%d}'%(num) + tab + vvaelu + '\n')
                    f.write('${Option_Value%d}'%(num) + tab + vvaelu + '\n')
                #valstr += ('@{Option_Value%d}'%(num) + tab + str(float(table.cell(nrow, ncols-1).value)) + '\n')
                f.write('${Option_Index%d}'%(num) + tab + '0' + '\n')
                #f.write(valstr)
                num = num + 1
                # print uid
                # stime = table.cell(nrow, 1).value   #第二列的值
                # print stime


def main():
    excel_table_byname()

if __name__=="__main__":
    main()
