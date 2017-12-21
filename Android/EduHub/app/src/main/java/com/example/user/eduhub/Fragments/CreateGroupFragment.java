package com.example.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

import com.example.user.eduhub.Adapters.GroupAdapter;
import com.example.user.eduhub.Adapters.SpinnerAdapter;
import com.example.user.eduhub.Classes.Group;
import com.example.user.eduhub.Classes.TypeOfEducation;
import com.example.user.eduhub.Fakes.FakeGroupActivities;
import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by user on 21.12.2017.
 */

public class CreateGroupFragment extends Fragment {
Group newGroup;
ArrayList<String> tags=new ArrayList<>();
TypeOfEducation type;
boolean privacy;
FakeGroupActivities fakeGroupActivities=new FakeGroupActivities();
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        final View v = inflater.inflate(R.layout.registration_fragment, null);
        final EditText nameOfGroup=v.findViewById(R.id.name);
        final EditText maxParticipants=v.findViewById(R.id.participants);
        final EditText cost=v.findViewById(R.id.cost);
        final Spinner typeOfEducation=v.findViewById(R.id.type_of_education);
        final EditText editTags=v.findViewById(R.id.tags);
        final CheckBox checkBox=v.findViewById(R.id.checkBox);
        Button createGroup=v.findViewById(R.id.create_group);
createSpenner(typeOfEducation);
        createGroup.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
              if(!nameOfGroup.getText().toString().equals("")&&!cost.getText().toString().equals("")&&!maxParticipants.getText().toString().equals("")&&type!=null&&!editTags.getText().toString().equals("")){
                 String[] str=editTags.getText().toString().split(" ");
                  for (String tag:str) {
                      tags.add(tag);
                  }
                  if (checkBox.isChecked()) {
                      privacy=true;
                  }else{
                      privacy=false;
                  }

                  newGroup=new Group(nameOfGroup.getText().toString(),Integer.parseInt(maxParticipants.getText().toString()),tags,Integer.parseInt(cost.getText().toString()),type,privacy);
                  fakeGroupActivities.CreateGroup(newGroup);
              }else{
                  MakeToast("Ошибка.заполните все поля.");
              }
            }
        });


        return v;
    }
    private void MakeToast(String s){
        Toast toast = Toast.makeText(getActivity().getApplicationContext(),
                (s), Toast.LENGTH_LONG);
        toast.show();
    }

    private void createSpenner(Spinner spinner){
        SpinnerAdapter adapter=new SpinnerAdapter(getActivity().getApplicationContext(),R.layout.spenner_item,TypeOfEducation.values());
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinner.setAdapter(adapter);
        // заголовок
        spinner.setPrompt("Title");
        // выделяем элемент
        spinner.setSelection(2);
        // устанавливаем обработчик нажатия
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                type=TypeOfEducation.values()[position];

            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
            }
        });

    }
}
