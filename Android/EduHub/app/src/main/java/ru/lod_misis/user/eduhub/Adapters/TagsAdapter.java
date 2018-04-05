package ru.lod_misis.user.eduhub.Adapters;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by User on 07.02.2018.
 */

public class TagsAdapter extends RecyclerView.Adapter<TagsAdapter.TagsViewHolder>{
    ArrayList<String> tags;

    public TagsAdapter(ArrayList<String> tags){
        this.tags=tags;

    }
    public static class TagsViewHolder extends RecyclerView.ViewHolder{
        TextView tag;
        public TagsViewHolder(View itemView) {
            super(itemView);
            tag=itemView.findViewById(R.id.tag);
        }


    }
    @Override
    public TagsAdapter.TagsViewHolder onCreateViewHolder(ViewGroup parent, int i) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.tag_item, parent, false);
        TagsAdapter.TagsViewHolder gvh = new TagsAdapter.TagsViewHolder(v);
        return gvh;
    }

    @Override
    public void onBindViewHolder(TagsAdapter.TagsViewHolder holder, int i) {

       holder.tag.setText(tags.get(i));

    }

    @Override
    public int getItemCount() {
        return tags.size();
    }
}

