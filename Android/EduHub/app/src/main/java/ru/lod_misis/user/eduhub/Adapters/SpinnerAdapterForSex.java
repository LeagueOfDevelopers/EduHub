package ru.lod_misis.user.eduhub.Adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by User on 15.02.2018.
 */

public class SpinnerAdapterForSex extends ArrayAdapter<String> {
    private LayoutInflater mInflater;
    private int mLayout;
    private ArrayList<String> sex;
    public SpinnerAdapterForSex(Context context, int resource, ArrayList<String> sex) {
        super(context, resource,sex);
        this.sex=sex;
        this.mLayout = resource;
        this.mInflater = LayoutInflater.from(context);
    }@Override
    public View getDropDownView(int position, View convertView,
                                ViewGroup parent) {

        return getView(position, convertView, parent);
    }

    public View getView(int position, View convertView, ViewGroup parent) {

        View view= mInflater.inflate(this.mLayout, parent, false);

        TextView type=view.findViewById(R.id.spenner_item);

        type.setText(sex.get(position).toString());

        return view;
    }
}
