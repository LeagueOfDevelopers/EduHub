import React from 'react'
import styled from 'styled-components'


const ItemImage = styled.div`
    background: ${props => props.primary ? '#f0aa31' : '#a0fa00'} ;
    height: 100%;
    height: 60px;
    width: 60px;
    margin-right: 12px;
`
const Price = styled.span`
font-size: 1.5em;
margin-left: 50px;
`

export default function ListItem({name, price, primary}) {
    
    return (
        <div className = "CardWrap">
            <ItemImage primary = {primary} />
            <div>
                {name}
            </div>
            <Price>
                {price}
            </Price>
        </div>
    )
}

// const propts = {
//     name: 'asd',
//     price: 12
// }