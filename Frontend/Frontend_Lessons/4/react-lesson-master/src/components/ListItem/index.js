import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components';

const CardWrap = styled.div`
position: relative;
box-shadow: 0 12px 24px rgba(0,0,0,0.07);
min-height: 60px;
width: 100%;
background: white;
border-radius: 8px;
margin-bottom: 30px;
display: flex;
`
const ItemImage = styled.div`
    background: ${props => props.primary ? '#f0aa31' : '#a0fa00'} ;
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
        <CardWrap >
            <ItemImage primary={primary}  />
            <div>
                {name}
            </div>
            <Price>
                {price}
            </Price>
        </CardWrap>
    )
}

ListItem.propTypes = {
    name: PropTypes.string,
    price: PropTypes.number,
}

ListItem.defaultProps = {
    name: "xanax",
    price: 200
}

export const a = 'asd'
export const b = "sadas"