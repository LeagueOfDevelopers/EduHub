package com.example.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.example.user.eduhub.Adapters.GroupMembersAdapter;
import com.example.user.eduhub.Models.Group.Member;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by User on 04.01.2018.
 */

public class GroupMembersFragment extends android.support.v4.app.Fragment {
    private ArrayList<Member> members;

    public void setMembers(ArrayList<Member> members) {
        this.members = members;
    }

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.group_members_fragment, null);
        RecyclerView recyclerView=v.findViewById(R.id.recycler);
        GroupMembersAdapter adapter=new GroupMembersAdapter(members);
        recyclerView.setHasFixedSize(true);
        LinearLayoutManager llm = new LinearLayoutManager(getActivity().getApplicationContext());
        recyclerView.setLayoutManager(llm);
        recyclerView.setAdapter(adapter);

        return v;
    }

}
