import { createSelector } from 'reselect';

/**
 * Direct selector to the groupPage state domain
 */
const selectGroupPage = (state) => state.get('groupPage');

export {
  selectGroupPage
};
