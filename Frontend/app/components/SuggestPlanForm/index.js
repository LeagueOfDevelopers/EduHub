/**
 *
 * SuggestPlanForm
 *
 */

import React from 'react';
import { createStructuredSelector } from 'reselect';
import { connect } from 'react-redux';
import { addPlan, acceptPlan, declinePlan } from "../../containers/GroupPage/actions";
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
                    <a href={`${config.API_BASE_URL}/file/${this.props.currentPlan}`} style={{color: '#0e0e0e'}} target='_blank' download=''>
                      <div style={{boxShadow: 'rgba(0, 0, 0, 0.4) 0px 0px 6px -3px', padding: 10, display: 'flex', justifyContent: 'center', alignItems: 'center', cursor: 'pointer'}}>
                        <img src={require('../../images/download.svg')} style={{width: 24, height: 24, marginRight: 10}}/>
                        План обучения
                      </div>
                    </a>
                  </Col>
                  : null
              }
            </Row>
            <Row type='flex' justify='space-around'>
              <Col className='group-btn plan-file-btn'>
                <Upload {...props} fileList={this.state.fileList}>
                  <Button type='primary' className='group-btn'>
                    <Icon type="upload" /> Выбрать файл
                  </Button>
                </Upload>
              </Col>
              <Col className='lg-center-container-item'>
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
          </Row>
        )
        : !this.props.isTeacher && this.props.curriculum ?
          <Row>
            {
              this.props.currentPlan ?
                <Row className='word-break' style={{marginBottom: 10}}>
                  <a href={`${config.API_BASE_URL}/file/${this.props.currentPlan}`} style={{color: '#0e0e0e'}} target='_blank' download=''>
                    <div style={{boxShadow: 'rgba(0, 0, 0, 0.4) 0px 0px 6px -3px', padding: 10, display: 'flex', justifyContent: 'center', alignItems: 'center', cursor: 'pointer'}}>
                      <img src={require('../../images/download.svg')} style={{width: 24, height: 24, marginRight: 10}}/>
                      План обучения
                    </div>
                  </a>
                </Row>
                : null
            }
            {
              this.props.members.find(item =>
                item.userId == this.props.currentUserData.UserId).curriculumStatus  === 0 ||
              this.props.members.find(item =>
              item.userId == this.props.currentUserData.UserId).curriculumStatus  === 1 ?
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
