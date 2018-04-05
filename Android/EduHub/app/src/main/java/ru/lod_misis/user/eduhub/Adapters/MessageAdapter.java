package ru.lod_misis.user.eduhub.Adapters;

import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import ru.lod_misis.user.eduhub.Classes.Message;
import ru.lod_misis.user.eduhub.Models.User;
import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by User on 22.12.2017.
 */

public class MessageAdapter extends RecyclerView.Adapter<MessageAdapter.MessageViewHolder>{
    ArrayList<Message> messages;
    User user;
    public MessageAdapter(ArrayList<Message> messages,User user){
        this.messages=messages;
        this.user=user;
    }
    public static class MessageViewHolder extends RecyclerView.ViewHolder{
        CardView cv;
        TextView role;
        TextView name;
        TextView time;
        TextView messageText;
        public MessageViewHolder(View itemView) {
            super(itemView);
            cv=itemView.findViewById(R.id.card_of_message);
            role=itemView.findViewById(R.id.role);
            name=itemView.findViewById(R.id.name);
            time=itemView.findViewById(R.id.time);
            messageText=itemView.findViewById(R.id.message);
        }


    }
    @Override
    public MessageViewHolder onCreateViewHolder(ViewGroup parent, int i) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.message, parent, false);
        MessageAdapter.MessageViewHolder gvh = new MessageAdapter.MessageViewHolder(v);
        return gvh;
    }

    @Override
    public void onBindViewHolder(MessageAdapter.MessageViewHolder holder, int i) {

        holder.messageText.setText(messages.get(i).getTextMessage());
        holder.name.setText(messages.get(i).getSenderName());
        holder.role.setText(messages.get(i).getSenderRole());
        holder.time.setText(messages.get(i).getTime().toString());

    }

    @Override
    public int getItemCount() {
        return messages.size();
    }
}
