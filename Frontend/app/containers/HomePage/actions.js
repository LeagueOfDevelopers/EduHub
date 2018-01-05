/*
 *
 * HomePage actions
 *
 */

import {
  GET_UNASSEMBLED_GROUPS_ERROR,
  GET_UNASSEMBLED_GROUPS_SUCCESS,
  GET_UNASSEMBLED_GROUPS_START,
  GET_ASSEMBLED_GROUPS_ERROR,
  GET_ASSEMBLED_GROUPS_SUCCESS,
  GET_ASSEMBLED_GROUPS_START
} from './constants';

export const getUnassembledGroups = () => (
  {
    type: GET_UNASSEMBLED_GROUPS_START
  }
)

export const getUnassembledGroupsSuccess = (groups) => (
  {
    type: GET_UNASSEMBLED_GROUPS_SUCCESS,
    payload: groups
  }
)

export const getUnassembledGroupsError = (error) => (
  {
    type: GET_UNASSEMBLED_GROUPS_ERROR,
    payload: error
  }
)

export const getAssembledGroups = () => (
  {
    type: GET_ASSEMBLED_GROUPS_START
  }
)

export const getAssembledGroupsSuccess = (groups) => (
  {
    type: GET_ASSEMBLED_GROUPS_SUCCESS,
    payload: groups
  }
)

export const getAssembledGroupsError = (error) => (
  {
    type: GET_ASSEMBLED_GROUPS_ERROR,
    payload: error
  }
)


