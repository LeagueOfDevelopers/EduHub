import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Modal, Button } from 'antd';
import  LoginForm from '../../components/elementary/LoginForm';

class DefineUserModal extends React.Component {
  state = {
    loading: false,
    visible: false,
  }
  
  showModal = () => {
    this.setState({
      visible: true,
    });
  }
  handleOk = () => {
    this.setState({ loading: true });
    setTimeout(() => {
      this.setState({ loading: false, visible: false });
    }, 1000);
  }
  handleCancel = () => {
    this.setState({ visible: false });
  }
  render() {
    const { visible, loading } = this.state;
    return (
        <div>
        <Button type="primary" onClick={this.showModal}>
            Личный кабинет
        </Button>
        <Modal
          visible={visible}
          title="Войти"
          onOk={this.handleOk}
          onCancel={this.handleCancel}
          footer={[
            <Button key="back" size="large" onClick={this.handleCancel}>Отмена</Button>,
            <Button key="submit" type="primary" size="large" loading={loading} onClick={this.handleOk}>
              <Link to={`/users/${this.userId}`}>Войти</Link>
            </Button>,
          ]}
        >
          <LoginForm />
        </Modal>
      </div>
    );
  }
}

export default DefineUserModal;