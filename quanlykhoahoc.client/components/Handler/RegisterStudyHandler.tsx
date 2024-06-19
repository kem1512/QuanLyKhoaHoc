"use client";

import { Checkbox, Grid, Input, TextInput } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  RegisterStudyClient,
  RegisterStudyCreate,
  RegisterStudyUpdate,
  IRegisterStudyMapping,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton from "../../components/Helper/ActionButton";
import { CourseSelect, SubjectSelect, UserSelect } from "../Helper/AppSelect";

export default function RegisterStudyHandler({ id }: { id?: number }) {
  const RegisterStudyService = new RegisterStudyClient();

  const { data, mutate } = useSWR(
    `/api/registerStudy/${id}`,
    () => RegisterStudyService.getEntity(id),
    {
      revalidateIfStale: false,
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  const [registerStudy, setRegisterStudy] = useState<IRegisterStudyMapping>({
    userId: 0,
    courseId: 0,
    currentSubjectId: 0,
    isActive: false,
    doneTime: new Date(),
    registerTime: new Date(),
  });

  useEffect(() => {
    if (data) setRegisterStudy(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <UserSelect
            value={registerStudy.user}
            onChange={(e) =>
              setRegisterStudy((prev) => ({ ...prev, userId: e.id }))
            }
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <CourseSelect
            value={registerStudy.course}
            onChange={(e) =>
              setRegisterStudy((prev) => ({ ...prev, courseId: e.id }))
            }
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <SubjectSelect
            single={true}
            value={registerStudy.currentSubject}
            onChange={(e) =>
              setRegisterStudy((prev) => ({ ...prev, currentSubjectId: e.id }))
            }
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <Checkbox
            label="Đã Kích Hoạt"
            checked={registerStudy.isActive}
            onChange={() =>
              setRegisterStudy((prev) => ({
                ...prev,
                isActive: !prev.isActive,
              }))
            }
          />
        </Grid.Col>
        <Grid.Col span={12}>
          <ActionButton
            size="xs"
            action={() =>
              handleSubmit(
                () => {
                  return id
                    ? RegisterStudyService.updateEntity(
                        registerStudy.id,
                        registerStudy as RegisterStudyUpdate
                      )
                    : RegisterStudyService.createEntity(
                        registerStudy as RegisterStudyCreate
                      );
                },
                `${id ? "Sửa" : "Thêm"} Thành Công`,
                mutate
              )
            }
          >
            Xác Nhận
          </ActionButton>
        </Grid.Col>
      </Grid>
    </DashboardLayout>
  );
}
