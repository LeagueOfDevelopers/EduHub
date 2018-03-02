/**
 *
 * SuggestPlanForm
 *
 */

import React from 'react';
import { createStructuredSelector } from 'reselect';
import { connect } from 'react-redux';
import { addPlan, acceptPlan, declinePlan } from "../../containers/GroupPage/actions";
import {Col, Row, Button, Upload, Icon, message} from 'antd';
import config from "../../config";


class SuggestPlanForm extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      fileList: []
    };

    this.handleChange = this.handleChange.bind(this);
  }

  componentDidMount() {
    console.log(this.props.curriculum)
  }

  handleChange = (info) => {
    let fileList = info.fileList;

    fileList = fileList.slice(-1);

    fileList = fileList.filter((file) => {
      if (file.response) {
        return file.status === 'done';
      }
      return true;
    });

    this.setState({fileList: fileList});
  };

  render() {
    const props = {
      name: 'file',
      action: `${config.API_BASE_URL}/file`,
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      },
      accept: 'text/plain, .pdf',
      onChange: this.handleChange
    };

    return (
      this.props.isTeacher ?
        (
          <Row className='lg-center-container-item' type='flex' justify='flex-start'>
            {
              this.props.curriculum ?
                <div style={{backgroundColor: 'red', width: '100%', height: 20, marginBottom: 10}}>
                </div>
                :
                null
            }
            <Col xs={{span: 24}} lg={{span: 10}} xl={{span: 8}} xxl={{span: 6}} className='group-btn plan-file-btn'>
              <Upload {...props} fileList={this.state.fileList}>
                <Button type='primary' className='group-btn' style={{marginBottom: 10}}>
                  <Icon type="upload" /> Выбрать файл
                </Button>
              </Upload>
            </Col>
            <Col xs={{span: 24}} lg={{span: 14}} xl={{span: 16}} xxl={{span: 18}} className='lg-center-container-item'>
              <Button
                className='group-btn'
                type="primary"
                onClick={() => {
                  this.props.addPlan(this.props.groupId, this.state.fileList[0].response.filename)
                }}
                disabled={this.state.fileList.length === 0}
                style={{marginBottom: 10}}
              >
                Предложить учебный план
              </Button>
            </Col>
          </Row>
        )
        : !this.props.isTeacher && this.props.curriculum ?
          <Row>
            <Col style={{backgroundColor: 'blue', height: 20, marginBottom: 10}}>
            </Col>
            {
              this.props.members.find(item =>
                item.userId === this.props.currentUserData.UserId).curriculumStatus  === 0 ||
              this.props.members.find(item =>
              item.userId === this.props.currentUserData.UserId).curriculumStatus  === 1 ?
                (
                  <Row type='flex' justify='space-around'>
                    <Col>
                      <Button
                        type="primary"
                        style={{marginBottom: 10}}
                        onClick={() => this.props.acceptPlan(this.props.groupId)}
                      >
                        Принять учебный план
                      </Button>
                    </Col>
                    <Col>
                      <Button
                        style={{marginBottom: 10}}
                        onClick={() => this.props.declinePlan(this.props.groupId)}
                      >
                        Отклонить учебный план
                      </Button>
                    </Col>
                  </Row>
                )
                : null
            }
          </Row>
          : null

    );
  }
}

SuggestPlanForm.propTypes = {

};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    addPlan: (groupId, plan) => dispatch(addPlan(groupId, plan)),
    acceptPlan: (groupId) => dispatch(acceptPlan(groupId)),
    declinePlan: (groupId) => dispatch(declinePlan(groupId))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(SuggestPlanForm);
