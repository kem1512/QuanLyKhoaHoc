"use client";

import { Button, Grid, TextInput } from "@mantine/core";
import { useState } from "react";
import { AccountClient } from "../../web-api-client";
import ProfileLayout from "../../../components/Layout/ProfileLayout/ProfileLayout";
import { handleSubmit } from "../../../lib/helper";
import { toast } from "react-toastify";

export default function ChangePassword() {
  const [user, setUser] = useState({
    currentPassword: "",
    newPassword: "",
    reNewPassword: "",
  });

  return (
    <ProfileLayout>
      {user && (
        <Grid>
          <Grid.Col span={{ base: 12, lg: 6 }}>
            <TextInput
              label="Mật Khẩu Hiện Tại"
              placeholder="Nhập Mật Khẩu Hiện Tại"
              type="password"
              value={user.currentPassword}
              onChange={(e) =>
                setUser((prev) => ({
                  ...prev,
                  currentPassword: e.target.value,
                }))
              }
              labelProps={{ style: { marginBottom: 6 } }}
            />
          </Grid.Col>
          <Grid.Col span={{ base: 12, lg: 6 }}>
            <TextInput
              label="Mật Khẩu Mới"
              placeholder="Nhập Mật Khẩu Mới"
              type="password"
              value={user.newPassword}
              onChange={(e) =>
                setUser((prev) => ({
                  ...prev,
                  newPassword: e.target.value,
                }))
              }
              labelProps={{ style: { marginBottom: 6 } }}
            />
          </Grid.Col>
          <Grid.Col span={{ base: 12, lg: 6 }}>
            <TextInput
              label="Nhập Lại Mật Khẩu Mới"
              placeholder="Nhập Lại Mật Khẩu Mới"
              type="password"
              value={user.reNewPassword}
              onChange={(e) =>
                setUser((prev) => ({
                  ...prev,
                  reNewPassword: e.target.value,
                }))
              }
              labelProps={{ style: { marginBottom: 6 } }}
            />
          </Grid.Col>
          <Grid.Col span={12}>
            <Button
              size="xs"
              onClick={() =>
                {
                  if(user.newPassword !== user.reNewPassword){
                    toast.error("Mật Khẩu Phải Giống Nhau")
                  }else{
                    handleSubmit(() => {
                      return new AccountClient().changePassword(
                        user.currentPassword,
                        user.newPassword
                      );
                    }, "Sửa Thành Công")
                  }
                }
              }
            >
              Xác Nhận
            </Button>
          </Grid.Col>
        </Grid>
      )}
    </ProfileLayout>
  );
}
