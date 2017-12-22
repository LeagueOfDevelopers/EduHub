import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button, Icon } from 'antd';
import DefineUserModal from '../../containers/DefineUserModal/index';

class BackToLotsButton extends React.Component {
    static defaultProps = {
        userId: 1
    }
    handleClick = () => {
        console.log(this.props)
        const bet = document.getElementById('betInput').value
        console.log(bet)
        this.props.makeBetHandler(this.props.lotId, this.props.userId, bet)
    }
    render() {
        return (
        <div style={{ }}>
            <Button type="primary" 
                style={{ float: 'right', marginTop: '12%', marginRight: '20px' }}>
                <Link to={`users/${this.props.userId}`}>Личный кабинет</Link>
            </Button>
            </div>
        );
    }
}

export default BackToLotsButton;
