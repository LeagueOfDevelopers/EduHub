/**
 *
 * SuggestPlanForm
 *
 */

import React from 'react';
import { createStructuredSelector } from 'reselect';
import { connect } from 'react-redux';
import { addPlan, acceptPlan, declinePlan, finishCourse } from "../../containers/GroupPage/actions";
import {Col, Row, Button, Upload, Icon, message, Avatar} from 'antd';
import config from "../../config";


class SuggestPlanForm extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      fileList: []
    };

    this.handleChange = this.handleChange.bind(this);
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
      accept: ".xlsx,.xls,image/*,.doc, .docx,.ppt, .pptx,.txt,.pdf",
      onChange: this.handleChange
    };

    return (
      this.props.isTeacher ?
        (
          <Row>
            <Row>
              {
                this.props.currentPlan ?
                  <Col span={24} className='word-break' style={{marginBottom: 10}}>
                    <span style={{marginRight: 12}}>Файл:</span>
                    <a href={`${config.API_BASE_URL}/file/${this.props.currentPlan}`} target='_blank' download='plan'>
                      Скачать план обучения
                    </a>
                  </Col>
                  : null
              }
            </Row>
            {
              this.props.courseStatus === 0 || this.props.courseStatus === 1 ?
                <Row type='flex'>
                  <Col style={{marginRight: 12}}>
                    <Upload {...props} fileList={this.state.fileList}>
                      <Button type='primary' className='group-btn'>
                        <Icon type="upload" /> Выбрать файл
                      </Button>
                    </Upload>
                  </Col>
                  <Col>
                    <Button
                      className='group-btn'
                      type="primary"
                      onClick={() => {
                        this.props.addPlan(this.props.groupId, this.state.fileList[0].response.filename);
                        this.setState({fileList: []});
                      }}
                      disabled={this.state.fileList.length === 0}
                      style={{marginBottom: 10}}
                    >
                      Предложить учебный план
                    </Button>
                  </Col>
                </Row>
                : this.props.courseStatus === 2 ?
                  <Row type='flex'>
                    <Col style={{marginRight: 12}}>
                      <Button type='primary' className='group-btn' onClick={() => this.props.finishCourse(this.props.groupId)}>
                        Завершить курс
                      </Button>
                    </Col>
                  </Row>
                : null
            }
          </Row>
        )
        : !this.props.isTeacher && this.props.curriculum ?
          <Row>
            <Row>
              {
                this.props.currentPlan ?
                  <Col span={24} className='word-break' style={{marginBottom: 10}}>
                    <span style={{marginRight: 12}}>Файл:</span>
                    <a href={`${config.API_BASE_URL}/file/${this.props.currentPlan}`} target='_blank' download='plan'>
                      Скачать план обучения
                    </a>
                  </Col>
                  : null
              }
            </Row>
            {
              this.props.members.find(item =>
                item.userId == this.props.currentUserData.UserId) && this.props.members.find(item =>
                item.userId == this.props.currentUserData.UserId).curriculumStatus  === 0 ||
              this.props.members.find(item =>
                item.userId == this.props.currentUserData.UserId) && this.props.members.find(item =>
              item.userId == this.props.currentUserData.UserId).curriculumStatus  === 1 ?
                <Row type='flex'>
                  <Col>
                    <Button
                      type="primary"
                      style={{marginRight: 12, marginBottom: 10}}
                      onClick={() => this.props.acceptPlan(this.props.groupId)}
                    >
                      Принять учебный план
                    </Button>
                  </Col>
                  <Col>
                    <Button
                      onClick={() => this.props.declinePlan(this.props.groupId)}
                    >
                      Отклонить учебный план
                    </Button>
                  </Col>
                </Row>
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
    declinePlan: (groupId) => dispatch(declinePlan(groupId)),
    finishCourse: (groupId) => dispatch(finishCourse(groupId))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(SuggestPlanForm);
