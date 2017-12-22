import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button, Icon } from 'antd';

const ExitButton = () => {
    return (
        <Button type="primary" 
            style={{ float: 'right', top: '27%', marginRight: '20px' }}>
            <Link to='/'>Выйти</Link>
        </Button>
    );
}

export default ExitButton;