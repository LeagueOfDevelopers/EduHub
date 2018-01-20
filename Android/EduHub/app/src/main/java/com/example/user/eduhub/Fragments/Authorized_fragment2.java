package com.example.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.example.user.eduhub.Adapters.GroupAdapter;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Retrofit.EduHubApi;

import java.util.ArrayList;

/**
 * Created by User on 15.01.2018.
 */

public class Authorized_fragment2 extends Fragment {
    RecyclerView recyclerView;
    ArrayList<Group> groups=new ArrayList<>();

    EduHubApi eduHubApi;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_for_users_and_teachers, null);
        recyclerView=v.findViewById(R.id.recycler);
        recyclerView.setHasFixedSize(true);
        LinearLayoutManager llm = new LinearLayoutManager(getActivity().getApplicationContext());
        recyclerView.setLayoutManager(llm);

        GroupAdapter adapter=new GroupAdapter(groups,getActivity());

        recyclerView.setAdapter(adapter);




        return v;
    }
    public void setGroups(ArrayList<Group> groups){
        this.groups=groups;
    }
}
