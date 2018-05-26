package ru.lod_misis.user.eduhub.Adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.example.user.eduhub.R;

/**
 * Created by user on 21.12.2017.
 */

public class SpinnerAdapter extends ArrayAdapter<String> {
    private LayoutInflater mInflater;
    private int mLayout;
    private String[] typesOfEducations;
    public SpinnerAdapter( Context context, int resource, String[] typesOfEducations) {
        super(context, resource,typesOfEducations);
        this.typesOfEducations=typesOfEducations;
        this.mLayout = resource;
        this.mInflater = LayoutInflater.from(context);
    }@Override
    public View getDropDownView(int position, View convertView,
                                ViewGroup parent) {

        return getView(position, convertView, parent);
    }

    public View getView(int position, View convertView, ViewGroup parent) {

        View view= mInflater.inflate(this.mLayout, parent, false);

        TextView type=view.findViewById(R.id.spinner_item2);
        TextView type2=view.findViewById(R.id.spenner_item);
        if(type!=null) {
            type.setText(typesOfEducations[position].toString());
        }
        if(type2!=null){
            type2.setText(typesOfEducations[position].toString());
        }
        return view;
    }
}
