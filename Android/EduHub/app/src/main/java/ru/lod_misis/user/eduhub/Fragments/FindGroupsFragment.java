package ru.lod_misis.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.example.user.eduhub.R;

import java.util.ArrayList;

import ru.lod_misis.user.eduhub.Adapters.GroupAdapter;
import ru.lod_misis.user.eduhub.Models.Group.Group;

/**
 * Created by User on 05.04.2018.
 */

public class FindGroupsFragment extends Fragment {

    RecyclerView recyclerView;
    SwipeRefreshLayout swipeContainer;
    ArrayList<Group> groups;
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.find_groups_fragment, null);
        if(this.getArguments().containsKey("findGroups")){
            groups=(ArrayList<Group>) this.getArguments().get("findGroups");

        recyclerView=v.findViewById(R.id.recycler);
        recyclerView.setHasFixedSize(true);
        LinearLayoutManager llm = new LinearLayoutManager(getActivity().getApplicationContext());
        swipeContainer = (SwipeRefreshLayout) v.findViewById(R.id.swipeContainer);
        recyclerView.setLayoutManager(llm);
        GroupAdapter adapter=new GroupAdapter(groups,getActivity(),getContext());

        recyclerView.setAdapter(adapter);}
        return v;
    }
}
