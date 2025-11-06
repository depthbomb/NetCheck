#include "MainWindow.h"
#include <QLabel>
#include <QVBoxLayout>
#include <QApplication>
#include <QRandomGenerator>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent),
    m_hosts({
        "youtu.be",
        "youtube.com",
        "mail.google.com",
        "google.com",
        "gstatic.com",
        "yt3.ggpht.com",
        "i.ytimg.com",
        "googleapis.com",
    }),
    m_initialIcon(":icons/icon.ico"),
    m_degradedIcon(":icons/icon-degraded.ico"),
    m_onlineIcon(":icons/icon-online.ico"),
    m_offlineIcon(":icons/icon-offline.ico"),
    m_trayIcon(m_initialIcon, this),
    m_displayLabel("Probing...")
{

    m_probeTimer.setInterval(1000);
    m_probeTimer.setSingleShot(true);
    m_probeTimer.start();

    m_manager.setTransferTimeout(5000);

    {
        const auto trayMenu = new QMenu();
        const auto quitAction = trayMenu->addAction("&Quit");

        connect(quitAction, &QAction::triggered, this, &QApplication::quit);

        m_trayIcon.setToolTip("NetCheck");
        m_trayIcon.setContextMenu(trayMenu);
        m_trayIcon.show();
    }

    connect(&m_trayIcon, &QSystemTrayIcon::activated, this, &MainWindow::onTrayIconActivated);
    connect(&m_probeTimer, &QTimer::timeout, this, &MainWindow::onProbeTimerTimeout);
    connect(&m_manager, &QNetworkAccessManager::finished, this, &MainWindow::onManagerFinished);

    {
        m_display.setObjectName("Display");

        const auto layout = new QVBoxLayout();

        m_displayLabel.setAlignment(Qt::AlignCenter);

        layout->addWidget(&m_displayLabel);

        m_display.setLayout(layout);

        setCentralWidget(&m_display);
    }

    setMinimumSize(600, 400);
    setWindowIcon(m_initialIcon);
    setWindowTitle("NetCheck");
}

void MainWindow::resizeEvent(QResizeEvent *event) {
    const auto fontSize = std::max(9, std::min(width(), height())) / 4;

    m_displayLabel.setStyleSheet(QString("QLabel { font-size: %1px; }").arg(fontSize));

    QMainWindow::resizeEvent(event);
}

void MainWindow::closeEvent(QCloseEvent *event) {
    event->ignore();
    hide();
}

void MainWindow::onProbeTimerTimeout() {
    m_elapsedTimer.start();

    const auto idx = QRandomGenerator::global()->bounded(m_hosts.size());
    const auto host = m_hosts.at(idx);

    m_manager.get(
        QNetworkRequest("https://" + host + "/generate_204")
    );
}

void MainWindow::onManagerFinished(QNetworkReply *reply) {
    const auto elapsedTime = static_cast<int>(m_elapsedTimer.elapsed());

    if (reply->error() == QNetworkReply::NoError) {
        setWindowTitle(QString("[%1ms] NetCheck").arg(elapsedTime));
        if (elapsedTime >= 350) {
            reportDegradedStatus();
        } else {
            reportOnlineStatus();
        }
    } else {
        reportOfflineStatus();
    }

    const int interval = elapsedTime < 1000 ? 1000 - elapsedTime : 0;
    m_probeTimer.setInterval(interval);
    m_probeTimer.start();
    reply->deleteLater();
}

void MainWindow::onTrayIconActivated(const QSystemTrayIcon::ActivationReason reason) {
    if (reason == QSystemTrayIcon::DoubleClick) {
        if (isVisible()) {
            hide();
        } else {
            show();
        }
    }
}

void MainWindow::reportOnlineStatus() {
    setWindowIcon(m_onlineIcon);

    m_trayIcon.setIcon(m_onlineIcon);
    m_trayIcon.setToolTip("Online");

    m_display.setStyleSheet("#Display { background-color: #00be63; } #Display > QLabel { color: #fff; font-weight: bold; }");

    m_displayLabel.setText("Online");
}

void MainWindow::reportDegradedStatus() {
    setWindowIcon(m_degradedIcon);

    m_trayIcon.setIcon(m_degradedIcon);
    m_trayIcon.setToolTip("Degraded");

    m_display.setStyleSheet("#Display { background-color: #f7630c; } #Display > QLabel { color: #fff; font-weight: bold; }");

    m_displayLabel.setText("Degraded");
}

void MainWindow::reportOfflineStatus() {
    setWindowIcon(m_offlineIcon);
    setWindowTitle("[Offline] NetCheck");

    m_trayIcon.setIcon(m_offlineIcon);
    m_trayIcon.setToolTip("Offline");

    m_display.setStyleSheet("#Display { background-color: #e81123; } #Display > QLabel { color: #fff; font-weight: bold; }");

    m_displayLabel.setText("Offline");
}
