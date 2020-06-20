import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HomeLayoutComponent} from './layout/home-layout/home-layout.component';
import {AuthGuard} from './core/guard/auth.guard';
import {MainLayoutComponent} from './layout/main-layout/main-layout.component';


const routes: Routes = [
  {
    path: '',
    component: HomeLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./modules/home/home.module').then(m => m.HomeModule)
      },
      //{
      //  path: '',
      //  loadChildren: () =>
      //    import('./modules/auth/auth.module').then(m => m.AuthModule)
      //}
    ]
  },
  {
    path: 'calendar',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./modules/calendar/calendar.module').then(m => m.CalendarModule),
        canActivate: [AuthGuard]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
