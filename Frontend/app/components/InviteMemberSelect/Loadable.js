/**
 *
 * Asynchronously loads the component for InviteMemberSelect
 *
 */

import Loadable from 'react-loadable';

export default Loadable({
  loader: () => import('./index'),
  loading: () => null,
});
