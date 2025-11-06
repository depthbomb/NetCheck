#include "widgets/MainWindow.h"
#include <QApplication>

int main(int argc, char *argv[]) {
    QApplication a(argc, argv);

    MainWindow w;

    return QApplication::exec();
}
