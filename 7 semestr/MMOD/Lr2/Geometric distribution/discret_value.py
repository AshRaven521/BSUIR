from trash import Window
from PyQt5 import QtCore, QtGui, QtWidgets
from PyQt5.QtWidgets import *
import pyqtgraph as pg
import random
import math
from scipy import stats


class Ui_Dialog(object):
    x_points = []
    def setupUi(self, Dialog):
        Dialog.setObjectName("Dialog")
        Dialog.resize(428, 215)
        self.prob = QtWidgets.QTextEdit(Dialog)
        self.prob.setGeometry(QtCore.QRect(60, 10, 81, 31))
        self.prob.setObjectName("prob")
        self.bargraph = QtWidgets.QPushButton(Dialog)
        self.bargraph.setGeometry(QtCore.QRect(60, 100, 81, 31))
        self.bargraph.setObjectName("bargraph")
        self.label = QtWidgets.QLabel(Dialog)
        self.label.setGeometry(QtCore.QRect(30, 7, 111, 20))
        self.label.setObjectName("label")
        self.textBrowser = QtWidgets.QTextBrowser(Dialog)
        self.textBrowser.setGeometry(QtCore.QRect(160, 10, 261, 191))
        self.textBrowser.setObjectName("textBrowser")
        self.number_n = QtWidgets.QTextEdit(Dialog)
        self.number_n.setGeometry(QtCore.QRect(60, 50, 81, 31))
        self.number_n.setObjectName("number_n")
        self.label_2 = QtWidgets.QLabel(Dialog)
        self.label_2.setGeometry(QtCore.QRect(30, 50, 111, 20))
        self.label_2.setObjectName("label_2")
        self.add_functions()
        self.retranslateUi(Dialog)
        QtCore.QMetaObject.connectSlotsByName(Dialog)

    def retranslateUi(self, Dialog):
        _translate = QtCore.QCoreApplication.translate
        Dialog.setWindowTitle(_translate("Dialog", "Dialog"))
        self.bargraph.setText(_translate("Dialog", "Result"))
        self.label.setText(_translate("Dialog", "p ="))
        self.label_2.setText(_translate("Dialog", "n ="))


    def generate(self):
        p = float(self.prob.toPlainText())
        p /= 100
        q = 1 - p
        n = int(self.number_n.toPlainText())
        values = []
        probs = []
        x = []
        y = []
        for i in range(n):
            values.append(math.ceil(math.log10(random.random()) / math.log10(q)))
            probs.append(p * (q ** (i - 1)))
            x.append(i)
            y.append(0)
        for i in values:
            for j in range(len(probs)):
                if j == i:  # if i < probs[j]:
                    y[j] += 1
                    break
        for i in range(len(y)):
            y[i] /= n
        print(f"vals:{values}")
        print(f"y:{y}")
        return x, y, values

    def point_estimate(self, arr):
        p = float(self.prob.toPlainText()) / 100
        M = 1 / p
        D = (1 - p) / (p ** 2)
        point_M = sum(arr) / len(arr)
        #point_D = sum(((i - point_M) ** 2) for i in arr) / len(arr)
        point_D = (sum(((i ** 2) for i in arr)) - len(arr) * (point_M ** 2)) / (len(arr) - 1)
        return ["Theoretical M:", M], ["Point M", point_M], ["Theoretical D:", D], ["Point D:", point_D], \
               ["P estimation:", 1 / point_M]

    def interval_estimate(self, arr):
        M = 1 / 2
        D = 1 / 12
        point_M = sum(arr) / len(arr)
        point_D = (sum(((i ** 2) for i in arr)) - len(arr) * (point_M ** 2)) / (len(arr) - 1)
        trust = 0.95

        student = self.studentValue(1 - (1 - trust) / 2, len(arr) - 1)
        print(student)
        left_M, right_M = point_M - student * point_D / math.sqrt(len(arr)), point_M + \
                          student * point_D / math.sqrt(len(arr))

        n = len(arr)
        deltaleft = n * point_D / self.chiSquareValue((1 + trust) / 2, n)
        deltaright = n * point_D / self.chiSquareValue((1 - trust) / 2, n)
        return ["M interval:", [left_M, right_M]], ["D interval:", [deltaleft, deltaright]]

    def studentValue(self, alpha, freedom):
        return stats.t.ppf(alpha, freedom)

    def chiSquareValue(self, alpha, freedom):
        return stats.chi2.ppf(alpha, freedom)



    def make_bar(self):
        plot = pg.plot()
        x, y1, vals = self.generate()
        bargraph = pg.BarGraphItem(x=x, height=y1, width=0.2, brush='g')
        plot.addItem(bargraph)
        layout = QGridLayout()
        layout.addWidget(plot)
        res = ""
        TM, PM, TD, PD, PP = self.point_estimate(vals)
        interval_M, interval_D = self.interval_estimate(vals)
        res += str(TM) + "\n" + str(PM) + "\n" + str(TD) + "\n" + str(PD) + "\n" + str(PP)
        res += "\n" + str(interval_M)
        res += "\n" + str(interval_D)
        self.textBrowser.setText(res)

    def add_functions(self):
        self.bargraph.clicked.connect(lambda: self.make_bar())



if __name__ == "__main__":
    import sys
    app = QtWidgets.QApplication(sys.argv)
    Dialog = QtWidgets.QDialog()
    ui = Ui_Dialog()
    ui.setupUi(Dialog)
    Dialog.show()
    sys.exit(app.exec_())
