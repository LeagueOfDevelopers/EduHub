package ru.lod_misis.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.design.widget.BottomSheetBehavior;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.StaggeredGridLayoutManager;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Spinner;

import com.example.user.eduhub.R;
import com.mindorks.placeholderview.ExpandablePlaceHolderView;

import java.util.ArrayList;

import mabbas007.tagsedittext.TagsEditText;
import ru.lod_misis.user.eduhub.Adapters.Empty_adapters_for_search_groups;
import ru.lod_misis.user.eduhub.Adapters.GroupAdapter;
import ru.lod_misis.user.eduhub.Adapters.SpinnerAdapter;
import ru.lod_misis.user.eduhub.Classes.FiltresModel;
import ru.lod_misis.user.eduhub.Interfaces.View.IFindGroupsView;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Presenters.FIndGroupsPresenter;

/**
 * Created by User on 05.04.2018.
 */

public class FindGroupsFragment extends Fragment implements IFindGroupsView {

    RecyclerView recyclerView;
    SwipeRefreshLayout swipeContainer;
    ArrayList<Group> groups;
    FiltresModel filtresModel;
    EditText minCost;
    EditText maxCost;
    TagsEditText editTags;
    CheckBox privacy;
    Spinner type;
    ProgressBar progressBar;
    GroupAdapter adapter;
    Empty_adapters_for_search_groups adapter3;
    FIndGroupsPresenter findGroupsPresenter=new FIndGroupsPresenter(this);
    Boolean flag;

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.find_groups_fragment, null);
        minCost=v.findViewById(R.id.minCost);
        maxCost=v.findViewById(R.id.maxCost);
        editTags=v.findViewById(R.id.tagsEditText);
        privacy=v.findViewById(R.id.privacy);
        type=v.findViewById(R.id.type_of_education);
        progressBar=v.findViewById(R.id.progressBar);



        if(this.getArguments().containsKey("findGroups")&&this.getArguments().containsKey("filters")){

            groups=(ArrayList<Group>) this.getArguments().get("findGroups");

            filtresModel=(FiltresModel) this.getArguments().get("filters");
            String[] types={"","Лекция","Мастер класс","Cеминар"};
            SpinnerAdapter
                    adapter2=new SpinnerAdapter(getContext(),R.layout.spinner_item2, types);
            type.setAdapter(adapter2);
            // заголовок
            // выделяем элемент
            switch (filtresModel.getType()){
                case "":{type.setSelection(0);
                    break;}
                case "Лекция":{type.setSelection(1);
                    break;}
                case "Мастер класс":{type.setSelection(2);
                    break;}
                case "Семинар":{type.setSelection(3);
                    break;}
            }
            minCost.setText(filtresModel.getMinCost()+"");
            maxCost.setText(filtresModel.getMaxCost()+"");
            privacy.setChecked(filtresModel.getPrivacy());
            String[] tags=new String[filtresModel.getTags().size()];
            for(int i=0;i<filtresModel.getTags().size();i++){
                tags[i]=filtresModel.getTags().get(i);
            }
            editTags.setTags( tags);
            flag=filtresModel.getPrivacy();
            if(flag){
                filtresModel.setPrivacy(false);

                privacy.setButtonDrawable(R.drawable.ic_black_circle);
                flag=false;
            }else{
                filtresModel.setPrivacy(true);

                privacy.setButtonDrawable(R.drawable.ic_check_circle_black_24dp);
                flag=true;
            }
            // устанавливаем обработчик нажатия

            type.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                @Override
                public void onItemSelected(AdapterView<?> parent, View view,
                                           int position, long id) {
                    // показываем позиция нажатого элемента
                    filtresModel.setType(types[position]);

                    findGroupsPresenter.findGroupsWithFilters(filtresModel.getMinCost(),filtresModel.getMaxCost(),filtresModel.getTittle(),
                            filtresModel.getTags(),filtresModel.getType(),filtresModel.getPrivacy(),getContext());


                }
                @Override
                public void onNothingSelected(AdapterView<?> arg0) {
                }
            });
            minCost.addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence s, int start, int count, int after) {

                }

                @Override
                public void onTextChanged(CharSequence s, int start, int before, int count) {

                }

                @Override
                public void afterTextChanged(Editable s) {
                    if(!s.toString().equals("")){
                    filtresModel.setMinCost(Double.parseDouble(s.toString()));
                    findGroupsPresenter.findGroupsWithFilters(filtresModel.getMinCost(),filtresModel.getMaxCost(),filtresModel.getTittle(),
                            filtresModel.getTags(),filtresModel.getType(),filtresModel.getPrivacy(),getContext());}
                }
            });
            maxCost.addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence s, int start, int count, int after) {

                }

                @Override
                public void onTextChanged(CharSequence s, int start, int before, int count) {

                }

                @Override
                public void afterTextChanged(Editable s) {
                    if(!s.toString().equals("")){
                        filtresModel.setMaxCost(Double.parseDouble(s.toString()));
                    findGroupsPresenter.findGroupsWithFilters(filtresModel.getMinCost(),filtresModel.getMaxCost(),filtresModel.getTittle(),
                            filtresModel.getTags(),filtresModel.getType(),filtresModel.getPrivacy(),getContext());}
                }
            });
            privacy.setOnClickListener(click->{
                if(flag){
                    filtresModel.setPrivacy(false);
                    findGroupsPresenter.findGroupsWithFilters(filtresModel.getMinCost(),filtresModel.getMaxCost(),filtresModel.getTittle(),
                            filtresModel.getTags(),filtresModel.getType(),filtresModel.getPrivacy(),getContext());
                    privacy.setButtonDrawable(R.drawable.ic_black_circle);
                    flag=false;
                }else{
                    filtresModel.setPrivacy(true);
                    findGroupsPresenter.findGroupsWithFilters(filtresModel.getMinCost(),filtresModel.getMaxCost(),filtresModel.getTittle(),
                            filtresModel.getTags(),filtresModel.getType(),filtresModel.getPrivacy(),getContext());
                    privacy.setButtonDrawable(R.drawable.ic_check_circle_black_24dp);
                    flag=true;
                }
            });
            editTags.setOnKeyListener(new View.OnKeyListener() {
                @Override
                public boolean onKey(View v, int keyCode, KeyEvent event) {
                    if(keyCode==KeyEvent.KEYCODE_ENTER){
                        filtresModel.setTags((ArrayList<String>) editTags.getTags());
                        findGroupsPresenter.findGroupsWithFilters(filtresModel.getMinCost(),filtresModel.getMaxCost(),filtresModel.getTittle(),
                                filtresModel.getTags(),filtresModel.getType(),filtresModel.getPrivacy(),getContext());
                    }
                    return false;
                }
            });
        recyclerView=v.findViewById(R.id.recycler);
        recyclerView.setHasFixedSize(true);

        swipeContainer = (SwipeRefreshLayout) v.findViewById(R.id.swipeContainer);

            if(groups.size()==0){
                 adapter3=new Empty_adapters_for_search_groups();
                recyclerView.setAdapter(adapter3);
            }else{
        adapter=new GroupAdapter(groups,getActivity(),getContext());
                recyclerView.setAdapter(adapter);}

        }

        return v;
    }

    @Override
    public void showLoading() {
        if(progressBar.getVisibility()!=View.VISIBLE){recyclerView.setVisibility(View.GONE);
            progressBar.setVisibility(View.VISIBLE);}

    }

    @Override
    public void stopLoading() {
        if(progressBar.getVisibility()!=View.GONE){
        recyclerView.setVisibility(View.VISIBLE);
        progressBar.setVisibility(View.GONE);}
    }

    @Override
    public void getError(Throwable error) {

    }

    @Override
    public void getGroups(ArrayList<Group> groups) {
        if(groups.size()==0){
            adapter3=new Empty_adapters_for_search_groups();
            recyclerView.setAdapter(adapter3);
            LinearLayoutManager llm=new LinearLayoutManager(getContext());
            recyclerView.setLayoutManager(llm);
        }else{
            StaggeredGridLayoutManager llm = new StaggeredGridLayoutManager(2,1);
            recyclerView.setLayoutManager(llm);
            adapter=new GroupAdapter(groups,getActivity(),getContext());
            recyclerView.setAdapter(adapter);}
    }
}
