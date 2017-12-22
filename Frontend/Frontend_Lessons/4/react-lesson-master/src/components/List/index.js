import React from 'react';
import ListItem from '../ListItem'

export default function List({title, data}) {
 return (
     <div>
         <h1>{title}</h1>
         {data.map((dataFields, index )=> 
            <ListItem key={index} primary={index % 2 === 0} {...dataFields}/>
        )}
     </div>
 )
}