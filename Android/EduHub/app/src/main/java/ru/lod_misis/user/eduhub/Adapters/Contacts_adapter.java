package ru.lod_misis.user.eduhub.Adapters;

import android.app.Activity;
import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import ru.lod_misis.user.eduhub.Interfaces.IRefreshList;

import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by User on 15.02.2018.
 */

public class Contacts_adapter extends RecyclerView.Adapter<Contacts_adapter.ContactViewHolder>{
    private ArrayList<String> contacts;
    private Activity activity;
    private Context context;
    private IRefreshList refreshList;
    public Contacts_adapter(ArrayList<String> contacts, Activity activity,Context context,IRefreshList refreshList){
        this.contacts=contacts;
        this.activity=activity;
        this.context=context;
        this.refreshList=refreshList;
    }
    @Override
    public Contacts_adapter.ContactViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.contact_item, parent, false);
        Contacts_adapter.ContactViewHolder gvh = new Contacts_adapter.ContactViewHolder(v);
        return gvh;
    }

    @Override
    public void onBindViewHolder(Contacts_adapter.ContactViewHolder holder, final int position) {

        holder.contactText.setText(contacts.get(position));
        holder.deleteContact.setOnClickListener(click->{
            contacts.remove(position);
            refreshList.refreshContacts(contacts);
        });
    }

    @Override
    public int getItemCount() {
        return contacts.size();
    }
    @Override
    public void onAttachedToRecyclerView(RecyclerView recyclerView) {
        super.onAttachedToRecyclerView(recyclerView);
    }

    public static class ContactViewHolder extends RecyclerView.ViewHolder {
     ImageView label;
     TextView contactText;
     ImageView deleteContact;

        public ContactViewHolder(View itemView){
            super(itemView);
            contactText=itemView.findViewById(R.id.contact_text);
            deleteContact=itemView.findViewById(R.id.delte_contact);



        }




    }

}
