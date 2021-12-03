import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateChefComponent } from './create-chef.component';

describe('AutoCompleteComponent', () => {
  let component: CreateChefComponent;
  let fixture: ComponentFixture<CreateChefComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateChefComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateChefComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
