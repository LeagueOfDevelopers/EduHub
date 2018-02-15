package com.example.user.eduhub.Dialog;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;

import com.example.user.eduhub.R;

/**
 * Created by User on 03.02.2018.
 */

public class CreateDialog {
    private Context context;
    private DialogInterface.OnClickListener myClickListener;

    public CreateDialog(Context context, DialogInterface.OnClickListener myClickListener) {
        this.context = context;
        this.myClickListener=myClickListener;
    }

    public Dialog onCreateDialog(int id) {
        if (id == 1) {
            AlertDialog.Builder adb = new AlertDialog.Builder(context);
            // заголовок
            adb.setTitle(R.string.signInToGroup);
            // сообщение
            adb.setMessage(R.string.areShureSignInToGroup);
            // иконка

            // кнопка отрицательного ответа
            adb.setNegativeButton(R.string.no, myClickListener);
            // кнопка положительного ответа
            adb.setPositiveButton(R.string.yes, myClickListener);


            adb.setNeutralButton(R.string.cansel,myClickListener);
            // создаем диалог
            return adb.create();
        }
        if(id==2){
            AlertDialog.Builder adb = new AlertDialog.Builder(context);
            // заголовок
            adb.setTitle(R.string.exit);
            // сообщение
            adb.setMessage(R.string.areShureExit);
            // иконка
            // кнопка отрицательного ответа
            adb.setNegativeButton(R.string.no, myClickListener);
            // кнопка положительного ответа
            adb.setPositiveButton(R.string.yes, myClickListener);
            return adb.create();
        }
        return null;
    }
}
