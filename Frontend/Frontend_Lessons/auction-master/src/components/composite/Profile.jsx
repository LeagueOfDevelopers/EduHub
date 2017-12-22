import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import { Row, Col } from 'antd';

class Profile extends React.Component {
    constructor(props) {
        super(props);
    }
    static defaultProps = {
        login: 'undefined',
        balance: 0
    }
    static PropTypes = {
        user: PropTypes.object,
        login: PropTypes.string,
        balance: PropTypes.number
    }
    render() {
        return(
            <div>
            <Row style={{ marginBottom: '15px', display: 'flex' }}>
                <Col span={24}>
                    <h2 style={{ color: '#fff' }}>Логин: {this.props.user.name}</h2>
                    <Link to='#'>Изменить пароль</Link>
                </Col>
            </Row>
            <hr/>
            <Row style={{ marginBottom: '15px', display: 'flex' }}>
                <Col span={24}>
                    <h2 style={{ color: '#fff' }}>Текущий счёт: {this.props.user.account}</h2>
                    <Link to='#'>Пополнить</Link>
                </Col>
            </Row>
            <hr/>
            </div>
        )
    }
}

export default Profile;
