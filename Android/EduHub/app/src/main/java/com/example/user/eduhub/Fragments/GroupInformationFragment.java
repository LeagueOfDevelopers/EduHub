package com.example.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;


import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.GroupInfo;
import com.example.user.eduhub.R;



/**
 * Created by User on 05.01.2018.
 */

public class GroupInformationFragment extends Fragment {
    private Group group;
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        final View v = inflater.inflate(R.layout.group_information_fragment, null);
        TextView members=v.findViewById(R.id.members);
        TextView cost=v.findViewById(R.id.cost);
        TextView tags=v.findViewById(R.id.tags);
        TextView discription=v.findViewById(R.id.discription);

       // members.setText(group.getUsersNow()+"/"+group.getMaxUsers());
        cost.setText("$"+group.getGroupInfo().getMoneyPerUser());
        tags.setText(group.getGroupInfo().getTags().toString());
        discription.setText(group.getGroupInfo().getDescription());
        return v;
    }

    public void setGroup(Group group) {
        this.group = group;
    }
}
