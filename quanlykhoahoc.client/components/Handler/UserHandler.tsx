"use client";

import { Checkbox, Grid, Select, TextInput } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  IUserMapping,
  UserClient,
  UserStatus,
  UserUpdate,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { use, useEffect, useState } from "react";
import useSWR from "swr";
import { DateTimePicker } from "@mantine/dates";
import {
  CertificateSelect,
  DistrictSelect,
  ProvinceSelect,
  RoleSelect,
  WardSelect,
} from "../Helper/AppSelect";
import ActionButton from "../Helper/ActionButton";
import RegisterStudyHandler from "./RegisterStudyHandler";

export default function UserHandler({ id }: { id?: number }) {
  const UserService = new UserClient();

  const { data, mutate } = useSWR(`/api/user/${id}`, () =>
    UserService.getEntity(id)
  );

  const [user, setUser] = useState<IUserMapping>({
    dateOfBirth: new Date(),
    username: "",
    email: "",
    fullName: "",
    isActive: true,
    userStatus: UserStatus.Active,
  });

  useEffect(() => {
    if (data) setUser(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <CertificateSelect
            value={user.certificate}
            onChange={(e) =>
              setUser((prev) => ({
                ...prev,
                certificateId: e.id,
                certificate: e,
              }))
            }
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <Select
            label="Trạng Thái"
            placeholder="Chọn Trạng Thái"
            defaultValue={user.userStatus ?? "Active"}
            data={Object.keys(UserStatus)}
          />
        </Grid.Col>
        <Grid.Col style={{ display: "flex", justifyContent: "space-between" }}>
          <Checkbox
            label="Kích Hoạt"
            checked={user.isActive}
            onChange={() =>
              setUser((prev) => ({ ...prev, isActive: !prev.isActive }))
            }
          />

          <ActionButton
            size="xs"
            action={() =>
              handleSubmit(
                () => {
                  return UserService.updateEntity(user.id, user as UserUpdate);
                },
                "Sửa Thành Công",
                mutate
              )
            }
          >
            Xác Nhận
          </ActionButton>
        </Grid.Col>
        {user.id && <RegisterStudyHandler userId={user.id} />}
      </Grid>
    </DashboardLayout>
  );
}
