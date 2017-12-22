import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button, Icon } from 'antd';

const BackToLotsButton = () => {
    return (
        <Button type="primary" style={{ marginLeft: 20 }}>
            <Link to='/'>
                <Icon type="left" />
                    Назад к лотам
            </Link>
        </Button>
    );
}

export default BackToLotsButton;
