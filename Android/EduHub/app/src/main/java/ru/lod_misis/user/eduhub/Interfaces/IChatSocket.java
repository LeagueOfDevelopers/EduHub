package ru.lod_misis.user.eduhub.Interfaces;

public interface IChatSocket {
    boolean isRun();
    void sendMessage(String Message);
    void listener();
    void stopClient();
    void createdConnection();
}
