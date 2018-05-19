package ru.lod_misis.user.eduhub.Adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.example.user.eduhub.R;

/**
 * Created by User on 07.02.2018.
 */

public class SpinnerAdapterForMemberRole extends ArrayAdapter<String> {
    private LayoutInflater mInflater;
    private int mLayout;
    private String[] memberRoles;
    public SpinnerAdapterForMemberRole(Context context, int resource, String[] memberRoles) {
        super(context, resource,memberRoles);
        this.memberRoles=memberRoles;
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

        type.setText(memberRoles[position].toString());

        return view;
    }
}