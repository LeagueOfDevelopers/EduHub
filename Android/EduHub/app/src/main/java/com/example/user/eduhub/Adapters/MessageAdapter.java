package com.example.user.eduhub.Adapters;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.user.eduhub.Classes.Message;
import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by User on 22.12.2017.
 */

public class MessageAdapter extends RecyclerView.Adapter<MessageAdapter.GroupViewHolder>{
    ArrayList<Message> messages;
    public MessageAdapter(ArrayList<Message> messages){
        this.messages=messages;
    }
    public static class GroupViewHolder extends RecyclerView.ViewHolder{

        TextView role;
        TextView name;
        TextView time;
        TextView messageText;
        public GroupViewHolder(View itemView) {
            super(itemView);
            role=itemView.findViewById(R.id.role);
            name=itemView.findViewById(R.id.name);
            time=itemView.findViewById(R.id.time);
            messageText=itemView.findViewById(R.id.message);
        }


    }
    @Override
    public GroupViewHolder onCreateViewHolder(ViewGroup parent, int i) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.message, parent, false);
        MessageAdapter.GroupViewHolder gvh = new MessageAdapter.GroupViewHolder(v);
        return gvh;
    }

    @Override
    public void onBindViewHolder(MessageAdapter.GroupViewHolder groupViewHolder, int i) {

    }

    @Override
    public int getItemCount() {
        return messages.size();
    }
}
