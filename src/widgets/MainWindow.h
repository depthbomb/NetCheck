#pragma once
#include <QMenu>
#include <QTimer>
#include <QLabel>
#include <QCloseEvent>
#include <QMainWindow>
#include <QNetworkReply>
#include <QElapsedTimer>
#include <QSystemTrayIcon>
#include <QNetworkAccessManager>

class MainWindow final : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = nullptr);

protected:
    void resizeEvent(QResizeEvent *event) override;
    void closeEvent(QCloseEvent *event) override;

public slots:
    void onProbeTimerTimeout();
    void onManagerFinished(QNetworkReply *reply);
    void onTrayIconActivated(QSystemTrayIcon::ActivationReason reason);

private:
    QElapsedTimer m_elapsedTimer;
    QTimer m_probeTimer;
    QNetworkAccessManager m_manager;
    QStringList m_hosts;
    QString m_lastHost;
    QIcon m_initialIcon;
    QIcon m_degradedIcon;
    QIcon m_onlineIcon;
    QIcon m_offlineIcon;
    QSystemTrayIcon m_trayIcon;
    QWidget m_display;
    QLabel m_displayLabel;

    void reportOnlineStatus();
    void reportDegradedStatus();
    void reportOfflineStatus();
};
