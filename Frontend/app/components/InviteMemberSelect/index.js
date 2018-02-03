/**
*
* InviteMemberSelect
*
*/

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { inviteMember } from "../../containers/GroupPage/actions";
import { getUsers } from "../../containers/Header/actions";
import { makeSelectUsers } from "../../containers/Header/selectors";
import {Dropdown, Button, Menu, Select, message} from 'antd';


class InviteMemberSelect extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      inviteVisible: false,
      selectValue: ''
    };

    this.tryInviteMember = this.tryInviteMember.bind(this);
    this.handleVisibleChange = this.handleVisibleChange.bind(this);
    this.handleSelectChange = this.handleSelectChange.bind(this);
  }

  handleVisibleChange = (flag) => {
    this.setState({ inviteVisible: flag });
  };

  handleSelectChange = (value) => {
    this.setState({selectValue: value});
    this.props.getUsers(value)
  };

  tryInviteMember(invitedId) {
    if(localStorage.getItem('without_server') !== 'true') {
      this.props.inviteMember(this.props.groupId, invitedId, 'Member')
    }
    else {
      message.success('Приглашение отправлено');
    }

    setTimeout(() => this.setState({selectValue: ''}), 0);
  }

  render() {
    return (
      <Dropdown
        overlay={(
          <Menu>
            <Menu.Item className='unhover' key='0'>
              <Select
                mode='combobox'
                className='unhover'
                style={{width: '100%'}}
                value={this.state.selectValue}
                onChange={this.handleSelectChange}
                placeholder='Введите имя пользователя'
                defaultActiveFirstOption={false}
                showArrow={false}
              >
                {this.props.users.map(item =>
                  <Select.Option key={item.email}>
                    <div onClick={() => this.tryInviteMember(item.id)}>{item.name}</div>
                  </Select.Option>)}
              </Select>
            </Menu.Item>
          </Menu>
        )}
        onVisibleChange={this.handleVisibleChange}
        visible={this.state.inviteVisible}
        trigger={['click']}
      >
        <Button
          size='large'
          style={{width: 280, marginLeft: -16}}
          type='primary'
        >
          Пригласить
        </Button>
      </Dropdown>
    );
  }
}

InviteMemberSelect.propTypes = {

};

InviteMemberSelect.defaultProps = {
  users: localStorage.getItem('withoutServer') === 'true' ?
    ['Первый пользователь', 'Второй пользователь'] : []
};

const mapStateToProps = createStructuredSelector({
  users: makeSelectUsers()
});

function mapDispatchToProps(dispatch) {
  return {
    getUsers: (name) => dispatch(getUsers(name)),
    inviteMember: (groupId, invitedId, role) => dispatch(inviteMember(groupId, invitedId, role))
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

export default withConnect(InviteMemberSelect);
