package com.example.user.eduhub.Models;

import android.app.Activity;
import android.os.Environment;
import android.util.Log;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

import okhttp3.ResponseBody;

import static android.content.ContentValues.TAG;

/**
 * Created by User on 09.03.2018.
 */

public class DecodeFile {
    private Activity activity;

    public DecodeFile(Activity activity) {
        this.activity = activity;
    }

    public File writeResponseBodyToDisk(ResponseBody body) {
        try {
            Log.d("tEST",Environment.getExternalStorageDirectory() + File.separator + "1.png");
            // todo change the file location/name according to your needs
            File avatar = new File(Environment.getExternalStorageDirectory() + File.separator +"1.jpeg");

            InputStream inputStream = null;
            OutputStream outputStream = null;

            try {
                byte[] fileReader = new byte[4096];

                long fileSize = body.contentLength();
                long fileSizeDownloaded = 0;

                inputStream = body.byteStream();
                outputStream = new FileOutputStream(avatar);

                while (true) {
                    int read = inputStream.read(fileReader);

                    if (read == -1) {
                        break;
                    }

                    outputStream.write(fileReader, 0, read);

                    fileSizeDownloaded += read;

                    Log.d(TAG, "file download: " + fileSizeDownloaded + " of " + fileSize);
                }

                outputStream.flush();

                return avatar;
            } catch (IOException e) {
                return null;
            } finally {
                if (inputStream != null) {
                    inputStream.close();
                }

                if (outputStream != null) {
                    outputStream.close();
                }
            }
        } catch (IOException e) {
            return null;
        }
    }
}
